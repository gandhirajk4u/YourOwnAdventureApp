namespace YourOwnAdventureApp.Models.Models
{
    public class AdventureUserDto
    {
        /// <summary>
        /// Gets or sets the Path Id
        /// </summary>
        [Required]
        public Guid AdventureId { get; set; }

        /// <summary>
        /// Gets or sets the User Id
        /// </summary>
        [Required]
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
