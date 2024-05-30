using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using Talabat.Services.AI;

[ApiController]
[Route("api/[controller]")]
public class SentimentAnalysisController : ControllerBase
{
    private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;

    public SentimentAnalysisController(PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
    {
        _predictionEnginePool = predictionEnginePool;
    }

    [HttpPost]
    public ActionResult<ModelOutput> Predict([FromBody] ModelInput input)
    {
        var prediction = _predictionEnginePool.Predict("SentimentAnalysisModel", input);
        return Ok(prediction);
    }
}
