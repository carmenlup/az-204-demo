import { useEffect, useState } from "react";
import "./App.css";
import "@coreui/coreui/dist/css/coreui.min.css";
import "bootstrap/dist/css/bootstrap.min.css";
import Song from "./Song";
import Artist from "./Artist";

function App() {
  const [artists, setArtists] = useState([]);
  const [songs, setSongs] = useState([]);

  useEffect(function () {
    async function fetchArtists() {
      const artistsResponse = await fetch(`/api/Artists`);

      if (!artistsResponse.ok) throw new Error("Fetching artists failed");

      const getArtistsData = await artistsResponse.json();
      setArtists(getArtistsData);
      //console.log(getArtistsData);
    }

    fetchArtists();
  }, []);

  useEffect(function () {
    async function fetchSongs() {
      const songsResponse = await fetch(`/api/Songs`);

      if (!songsResponse) throw new Error("Fetching songs failed");

      const songsData = await songsResponse.json();
      setSongs(songsData);
      //console.log(songsData);
    }

    fetchSongs();
  }, []);

  return (
    <div>
      <Artist artists={artists} />
      <Song songs={songs} />
    </div>
  );
}

export default App;
