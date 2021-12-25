using System.Collections.Generic;

namespace Prikkerticker.Models.Twitter;

public class TwitterData
{
    public IEnumerable<Tweet> Data { get; set; } = new List<Tweet>();
}
