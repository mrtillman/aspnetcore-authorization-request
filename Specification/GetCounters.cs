using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Gherkin.Quick;
using Application;
using Domain;
using Common;
using Services;

namespace Specification
{

    [FeatureFile("./GetCounters.feature")]
    public sealed class GetCounters : TestBase
    {
        private readonly GetCountersUseCase usecase = new GetCountersUseCase(TestBase.MockCountersService());
        
        private Result<List<Counter>> countersResult { get; set; }

        [Fact]
        [Given("an access token")]
        public void An_access_token()
        {
            usecase.Token = _token;
        }
    
        [Fact]
        [When("I request counters")]
        private void I_request_counters(){
            countersResult = usecase.Execute().Result;
        }

        [Fact]
        [Then("I should receive counter data")]
	    private void I_should_receive_counter_data(){
            Assert.True(countersResult.DidSucceed);
        }
    }
}