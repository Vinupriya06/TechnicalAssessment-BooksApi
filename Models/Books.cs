using System.ComponentModel.DataAnnotations;

namespace BooksApi.Models
{
    public class Books
    {
        [Key]
        public int PublisherID { get; set; }
        public string? Publisher { get; set; }
        public string? Title { get; set; }
        public string? AuthorLastName { get; set; }
        public string? AuthorFirstName { get; set; }
        public decimal Price { get; set; }
        public string? Location { get; set; }
        public int YearOfPublication { get; set; }
        public string? Month { get; set; }
        public string? PageNumberRange { get; set; }
        public int VolumeNumber { get; set; }
        public string MLACitation
        {
            get
            {
                return $"{AuthorLastName}, {AuthorFirstName}. \"{Title}.\" {Publisher}, {Location}, {YearOfPublication}. {Price:C}.";
            }
        }

        public string ChicagoCitation
        {
            get
            {
                // Customize the citation format based on the provided data
                return $"{AuthorLastName}, {AuthorFirstName}. \"{Title}.\" {Publisher},{VolumeNumber}, {Month}, {YearOfPublication}, {PageNumberRange} .";
            }
        }
    }
}
