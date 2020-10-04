using System;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence
{
    public struct PostCategoryModelId
    {
        public string Name;
        public Guid CourseId;

        public PostCategoryModelId(string name, CourseModel course)
            : this(name, course.Id) { }

        public PostCategoryModelId(string name, Guid courseId)
        {
            Name = name;
            CourseId = courseId;
        }

        public override bool Equals(object? obj)
        {
            return obj is PostCategoryModelId other &&
                   Name.Equals(other.Name) &&
                   CourseId.Equals(other.CourseId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, CourseId);
        }

        public void Deconstruct(out string name, out Guid courseId)
        {
            name = Name;
            courseId = CourseId;
        }

        public static implicit operator (string Name, Guid CourseId)(PostCategoryModelId value)
        {
            return (value.Name, value.CourseId);
        }

        public static implicit operator PostCategoryModelId((string Name, Guid CourseId) value)
        {
            return new PostCategoryModelId(value.Name, value.CourseId);
        }
    }
}