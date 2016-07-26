using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Objects
{
    /// <summary>
    /// Represents an memory or packet tile.
    /// </summary>
    public class Tile
    {
        #region Vars
     
        private int objectCount;
        private uint address;
        private uint squareNumber;

        private Location location;
        private Location memoryLocation;

        private Item ground;
        private List<Item> items;
        private Client client;
        #endregion

        #region Constructors
        //memory tile contructors
        internal Tile(Client client, uint address, uint squareNumber, Location location)
            : this(client, address, squareNumber)
        {
            this.location = location;
        }

        internal Tile(Client client, uint address, uint squareNumber)
        {
        
            this.client = client;
            this.squareNumber = squareNumber;
            this.address = address;
            this.memoryLocation = squareNumber.ToMemoryLocation();

            this.ground = new Item(client, (int)client.Memory.ReadUInt32(address + client.Addresses.Map.DistanceTileItems + client.Addresses.Map.DistanceItemId));
        }

        //packet tile constructors
        internal Tile(Client client, uint groundId, Location location)
        {
           
            this.client = client;
            this.location = location;
            this.items = new List<Item>();

            if (groundId > 0)
                this.ground = new Item(client, (int)groundId);
        }
        #endregion

        #region Properties


        public Location Location
        {
            get { return location; }
            internal set { location = value; }
        }

        public Client Client
        {
            get { return client; }
        }

        public Item Ground
        {
            get { return ground; }
            set { ground = value; }
        }
        public uint TileNumber
        {
            get { return squareNumber; }
        }

        public uint Address
        {
            get { return address; }
        }

        public int ObjectCount
        {
            get
            {

                objectCount = client.Memory.ReadInt32(address + client.Addresses.Map.DistanceTileItemsCount);
                    return objectCount;
                
            }
        }
        public TileObject[] Objects
        {
            get
            {
                int count = ObjectCount;
                TileObject[] res = new TileObject[count];

                uint pointerItems = address + 44;
                uint pointerOrder = address + 4;

                for (int i = 0; i < count; i++)
                {
                    uint stackOrder = client.Memory.ReadUInt32(pointerOrder + i * 4);
                    uint current = pointerItems + stackOrder * client.Addresses.Map.StepTileObject;

                    res[i] = new TileObject(
                        client.Memory.ReadUInt32(current + 8),//itemId
                        client.Memory.ReadUInt32(current + 4), //Itemdata
                        client.Memory.ReadUInt32(current + 0),//itemdatex
                        stackOrder,this
                    );
                }

                return res;
            }
        }
      
        public Location MemoryLocation
        {
            get
            {
               
              return memoryLocation;
                
            }
            set { memoryLocation = value; }
        }

        public List<Item> Items
        {
            get
            {
                items = new List<Item>();

                foreach (TileObject tileObject in Objects)
                    items.Add(new Item(client, (int)tileObject.Id, (int)tileObject.Data, ItemLocation.FromLocation(location,(byte)tileObject.StackOrder)));

                return items;
            }
        }
        #endregion
        #region path finding
        private static Location AdjustLocation(Location loc, int xDiff, int yDiff)
        {
            int xNew = loc.X - xDiff;
            int yNew = loc.Y - yDiff;

            if (xNew > 17)
                xNew -= 18;
            else if (xNew < 0)
                xNew += 18;

            if (yNew > 13)
                yNew -= 14;
            else if (yNew < 0)
                yNew += 14;

            return new Location(xNew, yNew, loc.Z);
        }
        public bool IsReachable()
        {
            IEnumerable<Tile> tileList = client.Map.GetTilesSameFloor();
            Tile playerTile = client.Map.GetPlayerTile();
            Tile creatureTile = this;

            if (playerTile == null || creatureTile == null)
                return false;

            int xDiff, yDiff;
            uint playerZ = (uint)client.PlayerLocation.Z;
            var creatures = client.Battlelist.GetCreatures().Where(c => c.Z == playerZ);
            uint playerId = client.Memory.ReadUInt32(client.Addresses.Player.Id);

            xDiff = (int)playerTile.MemoryLocation.X - 8;
            yDiff = (int)playerTile.MemoryLocation.Y - 6;

            playerTile.MemoryLocation = AdjustLocation(playerTile.MemoryLocation, xDiff, yDiff);
            creatureTile.MemoryLocation = AdjustLocation(creatureTile.MemoryLocation, xDiff, yDiff);

            foreach (Tile tile in tileList)
            {
                tile.MemoryLocation = AdjustLocation(tile.MemoryLocation, xDiff, yDiff);

                if (tile.Ground.ItemData.Blocking || tile.Ground.ItemData.BlocksPath ||
                    tile.Items.Any(i => i.ItemData.Blocking || i.ItemData.BlocksPath || client.PathFinder.ModifiedItems.ContainsKey((uint)i.Id)) ||
                    creatures.Any(c => tile.Objects.Any(o => o.Data == c.Id && o.Data != playerId)))
                {
                    client.PathFinder.Grid[tile.MemoryLocation.X, tile.MemoryLocation.Y] = 0;
                }
                else
                {
                    client.PathFinder.Grid[tile.MemoryLocation.X, tile.MemoryLocation.Y] = 1;
                }
            }

            return client.PathFinder.FindPath(playerTile.MemoryLocation, creatureTile.MemoryLocation);
        }
      

        public bool IsBlocking()
        {

            if (this.Ground.ItemData.Blocking) {return true;}
            if (this.ground.ItemData.BlocksPath) { return true; }
            if(this.Items.Any(i => i.ItemData.Blocking || i.ItemData.BlocksPath)){return true;}
            if(client.Battlelist.GetScreenCreatures().Any(c => this.Objects.Any(o => o.Data == c.Id && o.Data != client.Player.Id))){return true;}          
                return false ;
           
        }
        public Item TopItem()
        {
            foreach (Item i in Items)
            {
                if(!i.ItemData.IsGround && !i.ItemData.TopOrder1 && !i.ItemData.TopOrder2 && !i.ItemData.TopOrder3 )
                {
                    return i;
                }
            }

            return Items[Items.Count() - 1];
        }
        #endregion

        #region Public Methods
        public bool ReplaceGround(uint newId)
        {

            return client.Memory.WriteUInt32(address + client.Addresses.Map.DistanceTileItems + client.Addresses.Map.DistanceItemId, newId);
        }

        public bool ReplaceObject(TileObject oldObject, TileObject newObject)
        {
            

            uint pointer = (uint)(address +
                (client.Addresses.Map.DistanceTileItems +
               client.Addresses.Map.StepTileObject * oldObject.StackOrder));

            return client.Memory.WriteUInt32(pointer + client.Addresses.Map.DistanceItemId, newObject.Id) &&
                client.Memory.WriteUInt32(pointer + client.Addresses.Map.DistanceItemData, newObject.Data) &&
                client.Memory.WriteUInt32(pointer + client.Addresses.Map.DistanceItemDataEx, newObject.DataEx);
        }
        #endregion
    }

    #region TileObject
    /// <summary>
    /// Represents an object on a memory Tile
    /// </summary>
    public class TileObject
    {
        public uint StackOrder { get; set; }
        public uint Id { get; set; }
        public uint Data { get; set; }
        public uint DataEx { get; set; }
        public Tile FromTile { get; set; }

        public TileObject(uint id, uint data, uint dataEx)
            : this(id, data, dataEx, 0, null) { }

        public TileObject(uint id, uint data, uint dataEx, uint stackOrder, Tile fromTile)
        {
            StackOrder = stackOrder;
            Id = id;
            Data = data;
            DataEx = dataEx;
            FromTile = fromTile;
        }
    }
    #endregion
}
