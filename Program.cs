using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DesignPatterns
{
    public class CodeBuilder
    {
        public Class classRef = new Class();
        public CodeBuilder(string className)
        {
            this.classRef.Name = className;
            this.classRef.Type = "class";
        }

        public CodeBuilder AddField(string name, string type)
        {
            this.classRef.Fields.Add(new Class(name, type));
            return this;
        }

        public override string ToString()
        {
            return this.classRef.ToString();
        }
    }

    public class Class
    {
        public Class()
        {

        }
        public Class(string name, string type)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<Class> Fields = new List<Class>();
        public int indentSize = 2;
        public override string ToString()
        {
            return ToStringImpl(0);
        }

        public string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            sb.Append($"{new string(' ', (indent * indentSize))}public {Type} {Name}");

            if (Type == "class")
                sb.AppendLine().AppendFormat("{0}{1}", new string(' ', (indent * indentSize)), "{").AppendLine();
            else if (Type != "class")
                sb.AppendLine(";");
            Fields.ForEach(f =>
            {
                sb.Append(f.ToStringImpl(indent + 1));
            });

            if (Type == "class")
                sb.AppendFormat("{0}{1}", new string(' ', (indent * indentSize)), "}");
            return sb.ToString();
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {

            var cb = new CodeBuilder("Person");//.AddField("Name", "string").AddField("Age","int");
            //.AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
            Console.ReadLine();
        }
    }
}