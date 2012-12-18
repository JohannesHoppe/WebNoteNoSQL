namespace WebNoteAOP.Tests.CacheImplementation
{
    public class YummySausageSalad
    {
        public int Key
        {
            get; 
            set;
        }
        
        public string Name
        {
            get;
            set;
        }

        #region Equality members

        public bool Equals(YummySausageSalad other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return other.Key == this.Key && Equals(other.Name, this.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof(YummySausageSalad))
            {
                return false;
            }
            return Equals((YummySausageSalad)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Key * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
            }
        }

        #endregion
    }
}
