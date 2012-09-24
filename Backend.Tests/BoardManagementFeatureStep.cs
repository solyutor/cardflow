using TechTalk.SpecFlow;

namespace Solyutor.CardFlow.Backend.Tests
{
    [Binding]
    public class BoardManagementFeatureStep
    {
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef

        [BeforeScenario]
        public void BeforeScenario()
        {
            PrepareBackend();
            PrepareScenario();

            //TODO: implement logic that has to run before executing each scenario
        }

        private void PrepareScenario()
        {
            throw new System.NotImplementedException();
        }

        private void PrepareBackend()
        {
            throw new System.NotImplementedException();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }

        [Given("I created new board named (.*)")]
        public void CreateNewBoard(string boardName)
        {

        }

        [Given("using following parameters:")]
        public void SetBoardParameters(Table table)
        {
            
        }

        [When("I save changes")]
        public void WhenIPressAdd()
        {

        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(string result)
        {
            //TODO: implement assert (verification) logic
        }
    }
}
