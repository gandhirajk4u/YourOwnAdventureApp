namespace YourOwnAdventureApp.Models.Models
{
    public class UserAdventureResponseModel
    {
        /// <summary>
        /// Gets or Sets the UserID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or Sets the AdventureId
        /// </summary>
        public Guid AdventureId { get; set; }

        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or Sets the Path
        /// </summary>
        public string? Path { get; set; }

    }
}
