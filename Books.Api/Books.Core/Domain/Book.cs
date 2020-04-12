namespace Books.Core.Domain
{
    using System;

    public class Book : Entity
    {
        public string Title { get; protected set; }
        public string Author { get; protected set; }
        public string Category { get; protected set; }
        public string PublishingCompany { get; protected set; }
        public string Description { get; protected set; }
        public int Pages { get; protected set; }

        protected Book()
        {
        }

        public Book(Guid id, string title, string author, string category, string publishingCompany, string description,
                    int pages)
        {
            Id = id;
            Title = title;
            Author = author;
            Category = category;
            PublishingCompany = publishingCompany;
            SetDescription(description);
            Pages = pages;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new Exception($"Book with id {Id} can not be empty. ");
            }

            Description = description;
        }
    }
}