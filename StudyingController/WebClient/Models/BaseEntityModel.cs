using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThinClient.Common;
using ThinClient.SCS;

namespace ThinClient.Models
{
    public abstract class BaseEntityModel<T> where T : BaseEntityDTO
    {
        public string EncryptedId
        {
            get;
            set;
        }

        public BaseEntityModel(T entityObject)
        {
            this.FillModel(entityObject);
        }

        public BaseEntityModel()
        { }

        public virtual void FillModel(T entityObject)
        {
            this.EncryptedId = Encryptor.Encrypt(entityObject.ID.ToString());
        }
    }
}