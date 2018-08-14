namespace Moona
{
    /// <summary>
    /// Either an int hash value or a maris-defined uint.
    /// </summary>
    internal class MKey : System.IComparable
    {
        //enum SpecialId { Reserved = 0 }

        public bool IsHash = false;
        public int Hash;
        public uint Id;

        public MKey(int hash)
        {
            IsHash = true;
            Hash = hash;
            Id = 0;
        }

        public MKey(uint id)
        {
            IsHash = false;
            Hash = 0;
            Id = id;
        }
        
        public override bool Equals(object obj)
        {
            var item = obj as MKey;

            if (item == null)
            {
                return false;
            }

            return this.CompareTo(obj) == 0;
        }

        public override int GetHashCode()
        {
            if(IsHash)
            {
                return Hash.GetHashCode();
            }
            else
            {
                return Id.GetHashCode();
            }
        }

        public int CompareTo(object o)
        {
            MKey a = (MKey)o;

            int d = (null == a) ? -1 : 0;

            if (0 == d)
            {
                d = (a.IsHash ? 1 : 0)
                  - (IsHash ? 1 : 0);

                if (0 == d)
                {
                    d = IsHash
                      ? a.Hash.CompareTo(Hash)
                      : a.Id.CompareTo(Id);
                }
            }
            return d;
        }
    }
}