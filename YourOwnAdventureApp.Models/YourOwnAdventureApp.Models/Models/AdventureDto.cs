namespace YourOwnAdventureApp.Models.Models
{
    /// <summary>
    /// Adventure  DTO
    /// </summary>
    public class AdventureDto
    {
        /// <summary>
        /// Gets or sets the Adventure Id
        /// </summary>
        public string? AdventureId { get; set; }

        /// <summary>
        /// Gets or sets the Adventure Name
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the Path
        /// </summary>
        [Required]
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the Created By
        /// </summary>
        [Required]
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
