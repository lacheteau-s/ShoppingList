using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace ShoppingList.Tests
{
	public class PriorityOrderer : ITestCaseOrderer
	{
		public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
		{
			var sortedTests = new SortedDictionary<int, List<TTestCase>>();

			foreach (var test in testCases)
			{
				var priority = 0;
				var attributes = test.TestMethod.Method.GetCustomAttributes(typeof(PriorityAttribute).AssemblyQualifiedName);

				foreach (var attr in attributes)
					priority = attr.GetNamedArgument<int>("Priority");

				GetOrCreate(sortedTests, priority).Add(test);
			}

			foreach (var list in sortedTests.Keys.Select(p => sortedTests[p]))
			{
				// Order in alphabetical order if multiple test cases have the same priority.
				list.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));

				foreach (var test in list)
					yield return test;
			}
		}

		/// <summary>
		/// Adds a new key/value pair if the key does not alread exists and returns the value for the key.
		/// </summary>
		private static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
		{
			if (dictionary.TryGetValue(key, out var result))
				return result;

			result = new TValue();
			dictionary[key] = result;

			return result;
		}
	}
}
