using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KonjoBot.Forms;
namespace KonjoBot.Objects
{
   partial class Client
    {
       public class FormHelper
       {
           Client client;
        
           private HudForm m_hud;
           public bool CloseForms = false;

           public FormHelper(Client cl)
           {
               client = cl;           
               m_hud = null;
           }
                    
           public HudForm Hud
           {
               get
               {
                   if (m_hud == null)
                   {
                       m_hud = new HudForm(client);
                   }
                   return m_hud;
               }
           }


       }
    }
}
