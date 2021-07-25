namespace Yarn
{
    using System.Collections.Generic;

    /// <summary>
    /// Contains the built-in types available in the Yarn language.
    /// </summary>
    public static class BuiltinTypes
    {
        /// <summary>An undefined type.</summary>
        /// <remarks>This value is not valid except during compilation. It
        /// is used to represent values that have not yet been assigned a
        /// type by the type system.</remarks>
        internal const IType Undefined = null;

        /// <summary>Gets the type representing strings.</summary>
        public static IType String { get; } = new StringType();

        /// <summary>Gets the type representing numbers.</summary>
        public static IType Number { get; } = new NumberType();

        /// <summary>Gets the type representing boolean values.</summary>
        public static IType Boolean { get; } = new BooleanType();

        /// <summary>Gets the type representing any value.</summary>
        public static IType Any { get; } = new AnyType();

        /// <summary>
        /// Gets a dictionary that maps CLR types to their corresponding
        /// Yarn types.
        /// </summary>
        public static IReadOnlyDictionary<System.Type, Yarn.IType> TypeMappings { get; } = new Dictionary<System.Type, Yarn.IType>
        {
            { typeof(string), BuiltinTypes.String },
            { typeof(bool), BuiltinTypes.Boolean },
            { typeof(int), BuiltinTypes.Number },
            { typeof(float), BuiltinTypes.Number },
            { typeof(double), BuiltinTypes.Number },
            { typeof(sbyte), BuiltinTypes.Number },
            { typeof(byte), BuiltinTypes.Number },
            { typeof(short), BuiltinTypes.Number },
            { typeof(ushort), BuiltinTypes.Number },
            { typeof(uint), BuiltinTypes.Number },
            { typeof(long), BuiltinTypes.Number },
            { typeof(ulong), BuiltinTypes.Number },
            { typeof(decimal), BuiltinTypes.Number },
            { typeof(object), BuiltinTypes.Any },
        };

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> containing all built-in
        /// properties defined in this class.
        /// </summary>
        /// <value>The list of built-in type objects.</value>
        internal static IEnumerable<IType> AllBuiltinTypes
        {
            get
            {
                var propertyInfos = typeof(BuiltinTypes).GetProperties(System.Reflection.BindingFlags.Static);

                List<IType> result = new List<IType>();

                foreach (var propertyInfo in propertyInfos)
                {
                    var interfacesOfProperty = propertyInfo.PropertyType.GetInterfaces();
                    foreach (var interfaceType in interfacesOfProperty)
                    {
                        if (interfaceType == typeof(IType))
                        {
                            // Get the value of this property and add it
                            result.Add((IType)propertyInfo.GetValue(null));
                            break;
                        }
                    }
                }

                return result;
            }
        }
    }
}
