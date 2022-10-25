using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Holding.Base.Infrastructure.Helpers
{
    /// <summary>
    ///  
    /// </summary>
    public class EnumsMemberScanner
    {
        private List<EnumType> _enumTypes;

        /// <summary>
        /// Get list of enums with members.
        /// </summary>
        public List<EnumType> EnumTypes
        {
            get
            {
                if (_enumTypes == null) throw new NullReferenceException();
                return _enumTypes;
            }
        }

        /// <summary>
        /// Scan all enum and members in target assembly.
        /// </summary>
        /// <param name="assemblyName">dll assembly full name.</param>
        public void Scan(string assemblyName)
        {
            _enumTypes = new List<EnumType>();
            Assembly assembly = Assembly.LoadFrom(assemblyName);
            assembly.GetTypes().Where(t => t.IsEnum).ToList().ForEach(@enum =>
            {
                var enumEntity = new EnumType { Name = @enum.Name };

                @enum.GetFields(BindingFlags.Public | BindingFlags.Static).ToList().ForEach(member =>
                {
                    var attr = member.GetCustomAttribute(typeof(DescriptionAttribute), false);
                    enumEntity.Members.Add(new EnumMemeber
                    {
                        Name = member.Name,
                        Description = attr != null ? attr.GetType().GetProperty("Description").GetValue(attr, null).ToString() : string.Empty,
                        Code = (int)Convert.ChangeType(member.GetValue(null), typeof(int))
                    });
                });
                _enumTypes.Add(enumEntity);
            });
        }
    }

    public class EnumType
    {
        public EnumType()
        {
            Members = new List<EnumMemeber>();
        }

        public string Name { get; set; }

        public IList<EnumMemeber> Members { get; private set; }
    }

    public class EnumMemeber
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Code { get; set; }
    }
}
