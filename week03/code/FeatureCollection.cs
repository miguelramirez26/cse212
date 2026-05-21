using System.Text.Json.Serialization;

public class FeatureCollection
{
    // TODO Problem 5 - ADD YOUR CODE HERE
    // Create additional classes as necessary
    
    // The JSON root contains a list of features (earthquakes)
    public Feature[] Features { get; set; }
}

public class Feature
{
    // Each feature contains a properties block with earthquake data
    public Properties Properties { get; set; }
}

public class Properties
{
    // The magnitude of the earthquake (can be a decimal point number)
    public double Mag { get; set; }

    // The text describing where the earthquake happened
    public string Place { get; set; }
}
