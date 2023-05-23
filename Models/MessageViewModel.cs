namespace MyMVCapp.Models;

public class MessageViewModel
{
    public string? Weather { get; set; }
    public string? OpenAIAnswer { get; set; }

    public List<CosmosTodoApi.Models.QAItem>? Items { get; set; }
}
