
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ed.Shared.Entity;

namespace NordCar.Shared.Entity
{
     public class NcEntityId : EntityId<Guid>
    {
        #region Factories
        /// <summary>
        /// Create new BioEntityId with a random Guid.
        /// Used when creating new entities, which should be saved by a repository.
        /// </summary>
        public static NcEntityId CreateNew()
        {
            return CreateNew(Guid.NewGuid());
        }

        /// <summary>
        /// Used when requiring a User GUID for testing. This way we use the same Test User everywhere.
        /// </summary>
        public static NcEntityId CreateNewTestUserGuid()
        {
            return CreateNew(Guid.NewGuid());
        }

        /// <summary>
        /// Used when reconstituting existing entities.
        /// </summary>
        public static NcEntityId CreateNew(Guid id)
        {
            return new NcEntityId(id);
        }

        /// <summary>
        /// Is used for objects used as search parameters, so they are not confused with real entities.
        /// </summary>
        public static NcEntityId CreateNewEmpty()
        {
            return new NcEntityId(Guid.Empty);
        }
        #endregion Factories

        protected NcEntityId(Guid id) : base(id)
        {
        }

        public bool IsEmpty
        {
            get {if (Value == Guid.Empty) return true; else return false;} 
        }
     }
}
