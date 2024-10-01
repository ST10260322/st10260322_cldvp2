using System.ComponentModel.DataAnnotations;

namespace st10260322_cldvp2.Models // Replace with your actual namespace
{
    public class TableEntityModel
    {
        [Required]
        [Display(Name = "Table Name")]
        public string TableName { get; set; }

        [Required]
        [Display(Name = "Partition Key")]
        public string PartitionKey { get; set; }

        [Required]
        [Display(Name = "Row Key")]
        public string RowKey { get; set; }

        [Required]
        [Display(Name = "Data")]
        public string Data { get; set; }
    }
}
