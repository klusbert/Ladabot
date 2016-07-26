using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    namespace KonjoBot.Objects
    {
        public class Hotkey
        {
            #region Variables
            private Client client;
            private byte number;
            #endregion

            #region Constructor
            public Hotkey(Client client, byte number)
            {
                if (number < 0 || number > Adresses.Hotkey.MaxHotkeys)
                    throw new ArgumentOutOfRangeException("number", "Hotkey number must be between 0 and Addresses.Hotkey.MaxHotkeys.");
                this.client = client;
                this.number = number;
            }
            #endregion

            #region Properties
            public bool SendAutomatically
            {
                get
                {
                    return Convert.ToBoolean(client.Memory.ReadByte(
                        Adresses.Hotkey.SendAutomaticallyStart +
                        number * Adresses.Hotkey.SendAutomaticallyStep));
                }
                set
                {
                    client.Memory.WriteByte(
                        Adresses.Hotkey.SendAutomaticallyStart +
                        number * Adresses.Hotkey.SendAutomaticallyStep, Convert.ToByte(value));
                }
            }

            public string Text
            {
                get
                {
                    return client.Memory.ReadString(
                        Adresses.Hotkey.TextStart +
                        number * Adresses.Hotkey.TextStep);
                }
                set
                {
                    //set text
                    client.Memory.WriteString(
                        Adresses.Hotkey.TextStart +
                        number * Adresses.Hotkey.TextStep, value);
                    //reset objectID
                    client.Memory.WriteUInt32(
                        Adresses.Hotkey.ObjectStart +
                        number * Adresses.Hotkey.ObjectStep, 0);
                }
            }

            public uint ObjectId
            {
                get
                {
                    return client.Memory.ReadUInt32(
                        Adresses.Hotkey.ObjectStart +
                        number * Adresses.Hotkey.ObjectStep);
                }
                set
                {
                    //set objectID
                    client.Memory.WriteUInt32(
                        Adresses.Hotkey.ObjectStart +
                        number * Adresses.Hotkey.ObjectStep, value);
                    //reset text
                    client.Memory.WriteString(
                        Adresses.Hotkey.TextStart +
                        number * Adresses.Hotkey.TextStep, "");
                }
            }

            public Constants.HotkeyObjectUseType ObjectUseType
            {
                get
                {
                    return (Constants.HotkeyObjectUseType)client.Memory.ReadUInt32(
                        Adresses.Hotkey.ObjectUseTypeStart +
                        number * Adresses.Hotkey.ObjectUseTypeStep);
                }
                set
                {
                    client.Memory.WriteUInt32(
                        Adresses.Hotkey.ObjectUseTypeStart +
                        number * Adresses.Hotkey.ObjectUseTypeStep, (uint)value);
                }
            }

            public string Shortcut
            {
                get
                {
                    int keyNum = (number + 1) % 12;
                    if (keyNum == 0) keyNum = 12;
                    string key = "F" + keyNum;
                    string modifier = "";
                    switch (number / 12)
                    {
                        case 1:
                            modifier = "Shift + ";
                            break;
                        case 2:
                            modifier = "Control + ";
                            break;
                    }
                    return modifier + key;
                }
            }
            public void RestoreHotkey(Hotkey h)
            {
                this.ObjectId = h.ObjectId;
                this.ObjectUseType = h.ObjectUseType;
                this.SendAutomatically = h.SendAutomatically;
                this.Text = h.Text;
            }
            #endregion
        }
    }

