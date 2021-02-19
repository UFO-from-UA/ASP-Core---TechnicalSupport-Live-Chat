using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalSupport.Models;

namespace TechnicalSupport.AutoDialog
{
    public class AutoDialog
    {



        private Guid _id;


        public AutoDialog (Guid id) {
            
            _id =id;

        }

        public Message ReplyMessage ()
        {


            return new Message();

        }




    }
}
