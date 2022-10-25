using Holding.Base.Infrastructure.Enums;
using System.Collections.Generic;
using Interpreter = Holding.Base.Infrastructure.Translator;

namespace Holding.Base.Infrastructure.Constants
{
	public class BuiltIn
	{
		public static IEnumerable<string> Roles
		{
			get
			{
				return new[]
				{
					Interpreter.Translator.Translate(ResourceKey.Teacher),
					Interpreter.Translator.Translate(ResourceKey.Manager),
					Interpreter.Translator.Translate(ResourceKey.Assistant),
					Interpreter.Translator.Translate(ResourceKey.Personnel),
					Interpreter.Translator.Translate(ResourceKey.ITManager),
					Interpreter.Translator.Translate(ResourceKey.SchoolAdmin)
				};
			}
		}
	}
}
