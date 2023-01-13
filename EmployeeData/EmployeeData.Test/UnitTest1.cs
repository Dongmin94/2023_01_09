using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeData.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeData.Test
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var services = new ServiceCollection();

			var configuration = new ConfigurationBuilder().Build();
			// Act
			services.(configuration);

			// Assert
		}
	}
}
