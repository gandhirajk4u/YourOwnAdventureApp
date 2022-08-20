
using System.ComponentModel.DataAnnotations;

namespace YourOwnAdventureApp.Models.Models
{
    /// <summary>
    /// Adventure DB Model
    /// </summary>
   public class AdventureDbModel
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        [Key]
        public Guid AdventureId { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the Path
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the Created By
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the Updated By
        /// </summary>
        public Guid UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the Created Date
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the Updated Date
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

    }
}
