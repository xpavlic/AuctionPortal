using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using System;

namespace AuctionPortal.BusinessLayer.DataTransferObjects
{
    public class ProductDTO : DtoBase
    {

	    public string Name { get; set; }

        public string ProductImgUrl { get; set; }

        public Guid AuctionId { get; set; }

		public override string ToString() => Name;

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ProductDTO)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (Name != null ? Name.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (ProductImgUrl != null ? ProductImgUrl.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ AuctionId.GetHashCode();
				return hashCode;
			}
		}
	}
}
