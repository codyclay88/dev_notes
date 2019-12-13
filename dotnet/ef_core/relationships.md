Lets look at the following classes:

```csharp
// Blog.cs
public class Blog {
    public int Id { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; set; }
}
```

```csharp
// Post.cs
public class Post {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}
```

```csharp
// MyContext.cs
class MyContext : DbContext {
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .HasForeignKey(p => p.BlogId);
    }
}
```

Let's define the relationship:
- `Blog` is the principal entity. This is the entity that is used to store the values of the principal key property that the entity is related to. 
- `Post` is the dependent entity. This is the entity that contains the foreign key property. Sometimes referred to as the "child" of the relationship. 
- `Post.BlogId` is the foreign key. This is the property in the dependent entity that is used to store the values of the principal key property that the entity is related to. 
- `Blog.Id` is the principal key. This is the property that uniquely identifies the principal entity. This may be the primary key or an alternate key. 
- `Post.Blog` is a reference navigation property. This is a navigation property that holds a reference to a single related entity. 
- `Blog.Posts` is a collection navigation property. This is a navigation property that contains references to many related entities. 

## Conventions
By convention, a relationship will be created when there is a navigation property discovered on a type. A property is considered a navigation property if the type it points to can not be mapped as a scalar type by the current database provider. 

### Fully Defined Relationships
The most common pattern for relationships is to have navigation properties defined on both ends of the relationship and a foreign key property defined in the dependent entity class. 

This is the case for the example shown above, (repeated below). 
```csharp
// Blog.cs
public class Blog {
    public int Id { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; set; }
}
```

```csharp
// Post.cs
public class Post {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}
```

```csharp
// MyContext.cs
class MyContext : DbContext {
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .HasForeignKey(p => p.BlogId);
    }
}
```

### No Foreign Key Property
While it is recommended to have a foreign key property defined in the dependent entity class, it is not required. If no foreign key property is found, a shadow foreign key property will be introduced at the same time with the name `<navigation property name><principal key name>`.
Let's revisit the original example with this configuration:

```csharp
// Blog.cs
public class Blog {
    public int Id { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; set; }
}
```

```csharp
// Post.cs
public class Post {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Blog Blog { get; set; }
}
```

If we wanted to be explicit about the relationship, we could use the following configuration.  
```csharp
// MyContext.cs
class MyContext : DbContext {
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Posts);
    }
}
```

Or we could use the string overload of `HasForeignKey` to configure a Shadow property as a foreign key. 
```csharp
// MyContext.cs
class MyContext : DbContext {
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .HasForeignKey("BlogId);
    }
}
```

### Single Navigation Property
Including just one Navigation Property (no inverse navigation, and no foreign key property) is enough to have a relationship defined by convention. You can also have a single navigation property and a foreign key property. 

```csharp
// Blog.cs
public class Blog {
    public int Id { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; set; }
}
```

```csharp
// Post.cs
public class Post {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}
```

If we wanted to be explicit about the relationship, we could use the following configuration.  
```csharp
// MyContext.cs
class MyContext : DbContext {
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Blog>()
            .HasMany(b => b.Posts)
            .WithOne();
    }
}
```

### Without Navigation Property
You don't necessarily need to provide a navigation property. You can simply provide a Foreign Key on one side of the relationship. 

```csharp
// Blog.cs
public class Blog {
    public int Id { get; set; }
    public string Url { get; set; }
}
```

```csharp
// Post.cs
public class Post {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
}
```

```csharp
// MyContext.cs
class MyContext : DbContext {
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany()
            .HasForeignKey(p => p.BlogId);
    }
}
```



## Fluent API
To configure a relationship with the Fluent API, you start by identifying the navigation properties that make up the relationship. `HasOne` or `HasMany` identifies the navigation property on the entity type you are beginning the configuration on. You then chain a call to `WithOne` or `WithMany` to identify the inverse navigation. `HasOne`/`WithOne` are used for reference navigation properties and `HasMany`/`WithMany` are used for collection navigation properties. 

## Required and Optional Relationships
You can use the Fluent API to configure whether the relationship is required or optional. Ultimately this controls whether the foreign key property is required or optional. This is most useful when you are using a shadow state foreign key. If you have a foreign key in your entity class then the requiredness is determined based on whether or not the foreign key is required or optional. 

```csharp
// Blog.cs
public class Blog {
    public int Id { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; set; }
}
```

```csharp
// Post.cs
public class Post {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Blog Blog { get; set; }
}
```

```csharp
// MyContext.cs
class MyContext : DbContext {
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany()
            .IsRequired();
    }
}
```

## Cascade Delete
By convention, cascade delete will be set to *Cascade* for required relationships and *ClientSetNull* for optional relationships. *Cascade* means dependent entities are also deleted. *ClientSetNull* means that dependent entities that are not loaded into memory will remain unchanged and must be deleted manually, or updated to point to a valid principal entity. For entities, that are loaded into memory, EF Core will attempt to set the foreign key properties to null. 

You can use the Fluent API to configure the cascade delete behavior for a given relationship explicity. 

```csharp
// Blog.cs
public class Blog {
    public int Id { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; set; }
}
```

```csharp
// Post.cs
public class Post {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int? BlogId { get; set; }
    public Blog Blog { get; set; }
}
```

```csharp
// MyContext.cs
class MyContext : DbContext {
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

## Other Relationship Patterns

### One-to-one
One to one relationships have a reference navigation property on both sides. They follow the same convention as one-to-many relationships, but a unique index is introduced on the foreign key property to ensure only one dependent is related to each principal. 

```csharp
// Blog.cs
public class Blog {
    public int Id { get; set; }
    public string Url { get; set; }

    public BlogImage BlogImage { get; set; }
}
```

```csharp
// Post.cs
public class BlogImage {
    public int Id { get; set; }
    public byte[] Image { get; set; }
    public string Caption { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}
```

When configuring the relationship with the Fluent API, you use the `HasOne` and `WithOne` methods. 

```csharp
// MyContext.cs
class MyContext : DbContext {
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<BlogImage> BlogImages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Blog>()
            .HasOne(b => b.BlogImage)
            .WithOne(i => i.Blog)
            .HasForeignKey<BlogImage>(b => b.BlogId);
    }
}
```

### Many-to-many
Many-to-many relationships without an entity class to represent the join table are not yet supported. However, you can represent a many-to-many relationship by including an entity class for the join table and mapping two separate one-to-many relationships. 


```csharp
// Post.cs
public class Post {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public List<PostTag> PostTags { get; set; }
}
```

```csharp
// Tag.cs
public class Tag {
    public int Id { get; set; }

    public List<PostTag> PostTags { get; set; }
}
```

```csharp
// PostTag.cs
public class PostTag {
    public int PostId { get; set; }
    public Post Post { get; set; }

    public int TagId { get; set; }
    public Tag Tag { get; set; }
}
```

```csharp
// MyContext.cs
class MyContext : DbContext {
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<PostTag>()
            .HasKey(pt => new { pt.PostId, pt.TagId });
        
        builder.Entity<PostTag>()
            .HasOne(pt => pt.Post)
            .WithMany(p => p.PostTags)
            .HasForeignKey(pt => pt.PostId);
        
        builder.Entity<PostTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.PostTags)
            .HasForeignKey(pt => pt.TagId);
    }
}
```