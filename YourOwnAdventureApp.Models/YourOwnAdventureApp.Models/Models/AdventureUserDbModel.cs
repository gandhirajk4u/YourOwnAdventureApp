
using System.ComponentModel.DataAnnotations;

namespace YourOwnAdventureApp.Models.Models
{
    public class AdventureUserDbModel
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        [Key]
        public Guid AdventureUserId { get; set; }

        /// <summary>
        /// Gets or sets the Path Id
        /// </summary>
        public Guid AdventureId { get; set; }

        /// <summary>
        /// Gets or sets the User Id
        /// </summary>
        public Guid UserId { get; set; }

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
