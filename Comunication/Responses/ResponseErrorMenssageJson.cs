namespace MarmorariaProjeto.UseCases.ItemEstoque
{
    public class ResponseErrorMenssageJson
    {
        public List<string> Errors { get; private set; }

        public ResponseErrorMenssageJson(string message)
        {
            Errors = new List<string> { message };
        }

    }
}