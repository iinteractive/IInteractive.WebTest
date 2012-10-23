using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IInteractive.WebConsole.Results
{
    public class TestResultsFactory
    {
        public const string DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.ffffzzz";
        public const string TimeSpanFormat = "c";

        public TestRunType GenerateTestRun(string name, string description, DateTime start, DateTime finish, DateTime creation, DateTime queuing, List<HttpRequestResult> results)
        {
            if (results == null) return null;

            var testRun = new TestRunType()
                              {
                                  id = Guid.NewGuid().ToString(),
                                  name = name,
                                  runUser = System.Environment.UserName
                              };

            var testListId = Guid.NewGuid();
            var unitTestTypeId = Guid.NewGuid();

            var testSettings = GenerateTestSettings(name, description, unitTestTypeId);


            var testList = GenerateTestList("Web Crawl Test", testListId);
            var times = GenerateTimes(start, finish, creation, queuing);
            var summary = GenerateSummary(results);

            var unitTests = new List<UnitTestType>();
            var testEntries = new List<TestEntryType>();
            var testResults = new List<object>();
            var testResultTypes = new List<ItemsChoiceType3>();
            
            foreach(HttpRequestResult result in results)
            {
                var storage = string.Empty;
                var unitTestId = Guid.NewGuid();
                var executionId = Guid.NewGuid();

                unitTests.Add(GenerateUnitTest(string.Format("Browser = {0}, RequestUrl = {1}", result.BrowserUsed.Name, result.RequestUrl.AbsoluteUri),
                    storage, unitTestId, executionId,
                    result.GetType()));

                testEntries.Add(GenerateTestEntry(unitTestId, executionId, testListId));

                testResults.Add(GenerateUnitTestResult(executionId, unitTestId, unitTestTypeId, testListId, result));

                testResultTypes.Add(ItemsChoiceType3.UnitTestResult);
            }

            testRun.Items = new object[]
                                {
                                    testSettings, // TODO: TestSettings!
                                    times,
                                    summary,
                                    new TestRunTypeTestLists()
                                        {
                                            TestList = new TestListType[]
                                                {
                                                    testList
                                                }
                                                    
                                        },
                                    new TestEntriesType1()
                                        {
                                            TestEntry = testEntries.ToArray()
                                        },
                                    new ResultsType()
                                        {
                                            Items = testResults.ToArray(),
                                            ItemsElementName = testResultTypes.ToArray()
                                        }
                                };

            return testRun;
        }

        public TestSettingsType GenerateTestSettings(string name, string description, Guid testTypeId)
        {
            return new TestSettingsType()
                       {
                           name = name,
                           Description = description,
                           Execution = GenerateExecutionSettings(testTypeId)
                       };
        }

        public TestSettingsTypeExecution GenerateExecutionSettings(Guid testTypeId)
        {
            return new TestSettingsTypeExecution()
                       {
                           TestTypeSpecific = GenerateTestTypeSettings(testTypeId),
                           AgentRule = AgentRule
                       };
        }

        public TestSettingsTypeExecutionTestTypeSpecific GenerateTestTypeSettings(Guid testTypeId)
        {
            return new TestSettingsTypeExecutionTestTypeSpecific()
                       {
                           Items = new object[]
                                       {
                                           new AssemblyResolutionSettingsType()
                                               {
                                                   testTypeId = testTypeId.ToString(),
                                                   AssemblyResolution =
                                                       new AssemblyResolutionSettingsTypeAssemblyResolution()
                                                           {
                                                               TestDirectory =
                                                                   new AssemblyResolutionSettingsTypeAssemblyResolutionTestDirectory
                                                                   ()
                                                                       {
                                                                           useLoadContext = true
                                                                       }
                                                           }
                                               }
                                       },
                            ItemsElementName = new ItemsChoiceType2[]
                                                    {
                                                        ItemsChoiceType2.UnitTestRunConfig
                                                    }

                       };
        }

        public TestRunTypeTimes GenerateTimes(DateTime start, DateTime finish, DateTime creation, DateTime queuing)
        {
            return new TestRunTypeTimes()
                       {
                           start = start.ToString(DateTimeFormat),
                           finish = finish.ToString(DateTimeFormat),
                           creation = creation.ToString(DateTimeFormat),
                           queuing = queuing.ToString(DateTimeFormat)
                       };
        }

        public TestRunTypeResultSummary GenerateSummary(List<HttpRequestResult> results)
        {
            var summary = new TestRunTypeResultSummary();

            var successes = results.Count(result => result.Error == null);
            var failed = results.Count(result => result.Error != null && result.Error.HttpCode != 0);
            var errors = results.Count - successes - failed;

            var counters = new CountersType()
                               {
                                   total = results.Count,
                                   executed = results.Count,
                                   passed = successes,
                                   completed = results.Count,
                                   error = errors,
                                   failed = failed
                               };

            if (counters.total == counters.passed)
                summary.outcome = "Passed";
            else
            {
                summary.outcome = "Failed";
            }

            summary.Items = new object[]
                                {
                                    counters
                                };

            return summary;
        }

        public UnitTestType GenerateUnitTest(string name, string storage, Guid id, Guid executionId, Type testClass)
        {
            return new UnitTestType()
                       {
                           name = name,
                           id = id.ToString(),
                           storage = storage,
                           Items = new object[]
                                       {
                                           new BaseTestTypeExecution() {id = executionId.ToString()}
                                       },
                           TestMethod = GenerateTestMethod(name, testClass)
                       };
        }

        public UnitTestTypeTestMethod GenerateTestMethod(string name, Type testMethod)
        {
            return new UnitTestTypeTestMethod()
                       {
                           codeBase = Assembly.GetCallingAssembly().CodeBase,
                           className = testMethod.FullName,
                           name = name
                       };
        }

        public TestListType GenerateTestList(string name, Guid id)
        {
            return new TestListType()
                       {
                           name = name,
                           id = id.ToString()
                       };
        }

        public TestEntryType GenerateTestEntry(Guid testId, Guid executionId, Guid testListId)
        {
            return new TestEntryType()
                       {
                           testId = testId.ToString(),
                           executionId = executionId.ToString(),
                           testListId = testListId.ToString()
                       };
        }

        public UnitTestResultType GenerateUnitTestResult(Guid executionId, Guid testId, Guid unitTestTypeId, Guid testListId, HttpRequestResult result)
        {
            return GenerateUnitTestResult(executionId, testId, string.Format("Browser = {0}, RequestUrl = {1}", result.BrowserUsed.Name, result.RequestUrl.AbsoluteUri),
                                   ComputerName,
                                   result.End.Subtract(result.Start), result.Start, result.End, unitTestTypeId,
                                   PassFail(result), testListId, executionId,
                                   GenerateOutput(null, null, GenerateErrorInfo(result)));

        }

        public UnitTestResultType GenerateUnitTestResult(Guid executionId, Guid testId, string testName, string computerName,
            TimeSpan duration, DateTime startTime, DateTime endTime, Guid testType, string outcome, Guid testListId, Guid relativeResultsDirectory,
            OutputType output)
        {
            return new UnitTestResultType()
                       {
                           executionId = executionId.ToString(),
                           testId = testId.ToString(),
                           testName = testName,
                           computerName = computerName,
                           duration = duration.ToString(TimeSpanFormat),
                           startTime = startTime.ToString(DateTimeFormat),
                           endTime = endTime.ToString(DateTimeFormat),
                           testType = testType.ToString(),
                           outcome = outcome,
                           testListId = testListId.ToString(),
                           relativeResultsDirectory = relativeResultsDirectory.ToString(),
                           Items = new object[]
                                       {
                                           output
                                       }

                       };
        }

        public OutputType GenerateOutput(string standardOut, string standardError, OutputTypeErrorInfo errorInfo)
        {
            var output = new OutputType();

            if (!string.IsNullOrWhiteSpace(standardOut)) output.StdOut = standardOut;
            if (!string.IsNullOrWhiteSpace(standardError)) output.StdErr = standardError;
            if (errorInfo != null) output.ErrorInfo = errorInfo;

            return output;
        }

        public OutputTypeErrorInfo GenerateErrorInfo(string message, string stackTrace)
        {
            return new OutputTypeErrorInfo()
                       {
                           Message = message,
                           StackTrace = stackTrace
                       };
        }

        public OutputTypeErrorInfo GenerateErrorInfo(Exception error)
        {
            return GenerateErrorInfo(error.Message, error.StackTrace);
        }
        
        public OutputTypeErrorInfo GenerateErrorInfo(HttpRequestResult result)
        {
            if (!IsSuccess(result))
            {
                if (result != null && result.Error != null)
                {
                    if (result.Error.Error != null) return GenerateErrorInfo(result.Error.Error);

                    return
                        GenerateErrorInfo(
                            string.Format("HTTP Code: {0}\r\nMessage: {1}\r\nBrowser Name: {2}", result.Error.HttpCode, result.Error.Message, result.BrowserUsed.Name),
                            null);
                }

                string brokenLinksMessage = string.Format("Browser Used: {0}\r\n", result.BrowserUsed.Name);
                if (result.Links != null)
                {
                    foreach (var link in result.Links)
                    {
                        if ((link.IsBroken && !link.IsIgnored) && link.IsForbidden)
                        {
                            string brokenLinkMessage = string.Format("Broken and Forbidden Link Found.  The content is as follows.\n\t{0}\n", link.Content);
                            brokenLinksMessage += brokenLinkMessage;
                        }
                        else if ((link.IsBroken && !link.IsIgnored) && !link.IsForbidden)
                        {
                            string brokenLinkMessage = string.Format("Broken Link Found.  The content is as follows.\n\t{0}\n", link.Content);
                            brokenLinksMessage += brokenLinkMessage;
                        }
                        else if (!(link.IsBroken && !link.IsIgnored) && link.IsForbidden)
                        {
                            string brokenLinkMessage = string.Format("Forbidden Link Found.  The content is as follows.\n\t{0}\n", link.Content);
                            brokenLinksMessage += brokenLinkMessage;
                        }
                    }

                    return
                        GenerateErrorInfo(
                            brokenLinksMessage,
                            null);
                }

                return null;
            }
            else
                return null;
        }

        private string PassFail(HttpRequestResult result)
        {
            return IsSuccess(result) ? "Passed" : "Failed";
        }

        private bool IsSuccess(HttpRequestResult result)
        {
            if (result.Error != null)
            {
                return false;
            }

            if (result.Links != null)
            {
                foreach (var link in result.Links)
                {
                    if ((link.IsBroken && !link.IsIgnored) || link.IsForbidden)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static string ComputerName
        {
            get { return System.Environment.MachineName; }
        }

        private static AgentRuleType AgentRule
        {
            get
            {
                return new AgentRuleType() { name = "LocalMachineDefaultRole" };
            }
        }
    }
}
