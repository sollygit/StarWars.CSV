# StarWars Movies CSV

The application extracts StarWars movies and their ratings from a CSV file.
## Features

- **Movies:** Retrieve a list of movies with details such as title, year, type, poster, price, and active status.
- **Movie Ratings:** Access detailed ratings for each movie, including rating score, release date, runtime, genre, director, language, and metascore.
- **Movie Lookup:** Search for a specific movie by its unique identifier.
- **CSV Data Source:** Movie and rating data are loaded from a CSV files, making it easy to update or extend the dataset.

## Project Structure

- `StarWars.Service`: Contains the main service logic for loading, searching, and providing movie data.
- `StarWars.Model`: Defines the data models for movies and ratings.
- `StarWars.Repository`: Implements search and data access logic.
- `StarWars.CSV`: Contains CSV files with movie and rating data.
