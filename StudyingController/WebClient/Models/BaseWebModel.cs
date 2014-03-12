using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThinClient.Common;
using ThinClient.SCS;

namespace ThinClient.Models
{
    public abstract class BaseWebModel<T> where T : BaseRef
    {
        public string EncryptedId
        {
            get;
            set;
        }

        public BaseWebModel(T refObject)
        {
            this.FillModel(refObject);
        }

        public BaseWebModel()
        { }

        public virtual void FillModel(T refObject)
        {
            this.EncryptedId = Encryptor.Encrypt(refObject.ID.ToString());
        }

        public virtual BaseRef GetRefObject() 
        {
            return new BaseRef
            {
                ID = Convert.ToInt32(Encryptor.Decrypt(this.EncryptedId))
            };
        }
    }
}