using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.FluentBuilder
{

    public class QueryBuilder
    {
        public QueryExpression queryExpression = new QueryExpression();
        public QueryBuilder()
        {

        }

        public void AddColumns(string[] columns = null)
        {
            queryExpression.Columns = columns;
        }

        public void AddTable(string table)
        {
            queryExpression.Table = table;
        }

        public QueryBuilder AddFilter(string field, string value)
        {
            queryExpression.Filters.Add(new QueryExpression.FilterExpression(field, value));
            return this;
        }

        public override string ToString()
        {
            return queryExpression.ToString();
        }
    }

    public class QueryExpression
    {
        public string[] Columns { get; set; }
        public string Table { get; set; }
        public List<FilterExpression> Filters { get; set; } = new List<FilterExpression>();
        public class FilterExpression
        {
            public string Field { get; set; }
            public string Value { get; set; }
            public FilterExpression(string field, string value)
            {
                this.Field = field;
                this.Value = value;
            }
        }

        public string ToStringImpl()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("SELECT {0}", Columns != null && Columns.Length > 0 ? string.Join(',', Columns) : "*");
            sb.AppendLine("");
            sb.AppendLine($"From {Table}");
            sb.AppendJoin(' ', Filters.Select((filter, i) =>
            {
                var filterStringBuilder = new StringBuilder();
                var _operator = i == 0 ? "WHERE" : "AND";
                filterStringBuilder.AppendLine($"{_operator} {filter.Field} = '{filter.Value}'");
                return filterStringBuilder.ToString();
            }));
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl();
        }

    }
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine("Design Patterns");

    //        var query = new QueryBuilder();
    //        query.AddColumns(new string[] { "Name", "LastName" });
    //        query.AddTable("UserAccount");
    //        query.AddFilter("Address", "home")
    //            .AddFilter("Phone", "961 4654 546")
    //            .AddFilter("HomePhone", "112961 4654 546");
    //        Console.WriteLine(query.ToString());
    //        Console.ReadLine();
    //    }
    //}
}