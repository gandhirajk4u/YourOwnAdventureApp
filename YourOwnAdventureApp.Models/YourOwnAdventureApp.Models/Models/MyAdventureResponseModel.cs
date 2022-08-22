namespace YourOwnAdventureApp.Models.Models
{
    public  class MyAdventureResponseModel
    {
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

        /// <summary>
        /// Gets or Sets the User Adventure Selection
        /// </summary>
        public bool IsUserSelected { get; set; }
    }
}
