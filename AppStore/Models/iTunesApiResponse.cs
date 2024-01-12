using System.Collections.Generic;

public class iTunesApiResponse
{
    public Feed Feed { get; set; }
}

public class Feed
{
    public List<Entry> Entry { get; set; }
}

public class Entry
{
    public Id Id { get; set; }
    public Name Name { get; set; }
    public ReleaseDate ReleaseDate { get; set; }
    public Category Category { get; set; }
}

public class Id
{
    public string Label { get; set; }
}

public class Name
{
    public string Label { get; set; }
}

public class ReleaseDate
{
    public string Label { get; set; }
}

public class Category
{
    public Attributes Attributes { get; set; }
}

public class Attributes
{
    public string Term { get; set; }
}
