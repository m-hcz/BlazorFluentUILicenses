using System.ComponentModel.DataAnnotations;

namespace CoreBusiness
{
	public class License
	{
        public int Id { get; set; }

		[Required]
		[EmailAddress]
		public string ClientEmail { get; set; } = string.Empty;

		[Required]
		public DateTime CreatedDate { get; set; }
    }
}
