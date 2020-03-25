using System;

namespace Assioma.Ecomm.Domain
{
    public abstract partial class Entity<TId>
        where TId : struct, IComparable, IFormattable, IConvertible, IComparable<TId>, IEquatable<TId>
    {
        #region MEMBERS
        private int? _requestedHashCode;
        #endregion

        #region CTOR
        protected Entity() { } // EF
        #endregion

        #region Properties
        /// <summary>
        /// Get the persisten object identifier
        /// Is used as primary key.
        /// </summary>
        public virtual TId Id { get; protected set; }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Check if this entity is transient, ie, without identity at this moment
        /// </summary>
        /// <returns>True if entity is transient, else false</returns>
        public bool IsTransient()
        {
            return this.Id.Equals(default(TId));
        }
        #endregion

        #region Overrides Methods
        /// <summary>
        /// <see cref="M:System.Object.Equals" />
        /// </summary>
        /// <param name="obj">
        /// <see cref="M:System.Object.Equals" />
        /// </param>
        /// <returns>
        /// <see cref="M:System.Object.Equals" />
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TId>))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var item = (Entity<TId>)obj;

            if (item.IsTransient() || this.IsTransient())
            {
                return false;
            }
            else
            {
                return item.Id.Equals(this.Id);
            }
        }

        /// <summary>
        /// <see cref="M:System.Object.GetHashCode" />
        /// </summary>
        /// <returns>
        /// <see cref="M:System.Object.GetHashCode" />
        /// </returns>
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                {
                    _requestedHashCode = this.Id.GetHashCode();
                }
                return _requestedHashCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            if (Equals(left, null))
            {
                return (Equals(right, null)) ? true : false;
            }
            else
            {
                return left.Equals(right);
            }
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !(left == right);
        }
        #endregion
    }
}
