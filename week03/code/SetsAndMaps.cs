using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE
        HashSet<string> seenWords = new HashSet<string>();
        List<string> pairs = new List<string>();

        foreach (string word in words)
        {
            // Reverse the 2-letter word
            string reversed = $"{word[1]}{word[0]}";
            
            // Check if the symmetric partner has already been seen
            if (seenWords.Contains(reversed))
            {
                pairs.Add($"{reversed} & {word}");
            }
            else
            {
                // Add current word to the set for future matching
                seenWords.Add(word);
            }
        }
        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE

            // 1. Get the degree name (Column 4 = Index 3) and remove extra whitespace
            string degree = fields[3].Trim();

            // 2. Count the occurrences using the dictionary
            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE

        // 1. Create a dictionary to hold our character counts
        var letterCounts = new Dictionary<char, int>();

        // 2. Loop through each character of the first word in lowercase
        foreach (char c in word1.ToLower())
        {
            // We need to ignore spaces!
            if (c == ' ')
            {
                continue; // This skips the rest of the loop for this character
            }

            // Check if the letter is already in our dictionary
            if (letterCounts.ContainsKey(c))
            {
                letterCounts[c]++;
            }
            else
            {
                letterCounts[c] = 1;
            }
        }

        // 3. Loop through each character of the second word
        foreach (char c in word2.ToLower())
        {
            if (c == ' ')
            {
                continue; // Ignore spaces just like before
            }

            //  Check if the letter is NOT in our dictionary
            if (!letterCounts.ContainsKey(c))
            {
                return false; // Found a letter that shouldn't be there! Not an anagram.
            }
            else
            {
                letterCounts[c]--;
            }
        }
        
        // 4. Check if all counts are exactly 0
        foreach (var pair in letterCounts)
        {
            // If any letter count is not 0, it's not a perfect match!
            if (pair.Value != 0)
            {
                return false;
            }
        }
        
        // If we checked everything and all are 0, they ARE anagrams!
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.

        // 2. Create a list to hold the string descriptions
        var summaryList = new List<string>();

        // Loop through each earthquake feature in the collection
        foreach (var feature in featureCollection.Features)
        {
            // Build the string: "[place] - mag [magnitude]"
            string item = $"{feature.Properties.Place} - Mag {feature.Properties.Mag}";
            summaryList.Add(item);
        }

        // 3. Return the array of descriptions
        return summaryList.ToArray();
    }
}