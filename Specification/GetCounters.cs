using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using Xunit.Gherkin.Quick;
using Application;
using Common;
using Domain;

namespace Specification
{
    
    [FeatureFile("./GetCounters.feature")]
    public sealed class GetCounters : World
    {
        private readonly GetCountersUseCase usecase = new GetCountersUseCase(World.MockCountersService());
        
        private Result<List<Counter>> result { get; set; }

        [Given("an access token")]
        public void An_access_token()
        {
            usecase.Token = _token;
        }
    
        [When("I request counters")]
        public async Task I_request_counters(){
            result = await usecase.Execute();
        }

        [Then("I should receive counter data")]
	    public void I_should_receive_counter_data(){
            Assert.True(result.DidSucceed);
        }
    }
}