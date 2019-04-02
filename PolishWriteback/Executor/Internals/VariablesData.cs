using System;
using System.Collections.Generic;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.PolishWriteback.Executor.Exceptions;

namespace SmallScript.PolishWriteback.Executor.Internals
{
	internal sealed class VariablesData
	{
		private readonly IDictionary<VariableToken, int> _variables;

		public VariablesData()
		{
			_variables = new Dictionary<VariableToken, int>();
		}

		public int Create(VariableToken token, int value)
		{
			if (_variables.ContainsKey(token))
			{
				throw new VariableRedeclarationException(token);
			}
			
			_variables.Add(token, value);

			return value;
		}
		
		public int Create(VariableToken token, string value)
		{
			if (_variables.ContainsKey(token))
			{
				throw new VariableRedeclarationException(token);
			}

			var intValue = Int32.Parse(value);
			
			_variables.Add(token, intValue);

			return intValue;
		}

		public int Get(VariableToken token)
		{
			if (!_variables.ContainsKey(token))
			{
				throw new NoSuchVariableException(token);
			}

			return _variables[token];
		}

		public int Alter(VariableToken token, int newValue)
		{
			if (!_variables.ContainsKey(token))
			{
				throw new NoSuchVariableException(token);
			}

			_variables[token] = newValue;

			return newValue;
		}
		
		public int Alter(VariableToken token, string newValue)
		{
			if (!_variables.ContainsKey(token))
			{
				throw new NoSuchVariableException(token);
			}

			var value = Int32.Parse(newValue);
			
			_variables[token] = value;

			return value;
		}
	}
}