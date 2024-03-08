import { useEffect, useState } from "react";
import "./App.css";
import "@coreui/coreui/dist/css/coreui.min.css";
import "bootstrap/dist/css/bootstrap.min.css";
import Song from "./Song";
import Artist from "./Artist";
import TrendingSong from "./TrendingSong";

function App() {
  const [artists, setArtists] = useState([]);
  const [songs, setSongs] = useState([]);
  const [trendingSongs, setTrendingSongs] = useState([]);
  const [pageNumberSongs, setPageNumberSongs] = useState(1);
  const [pageNumberArtists, setPageNumberArtists] = useState(1);

  useEffect(
    function () {
      async function fetchArtists() {
        const getArtistsResponse = await fetch(
          `/api/Artists?pageNumber=${pageNumberArtists}&pageSize=5`
        );

        if (!getArtistsResponse) throw new Error("Fetching artists failed");

        const data = await getArtistsResponse.json();
        setArtists(data);
        //console.log(getArtistsData);
      }

      fetchArtists();
    },
    [pageNumberArtists]
  );

  useEffect(
    function () {
      async function fetchSongs() {
        const getSongsResponse = await fetch(
          `/api/Songs?pageNumber=${pageNumberSongs}&pageSize=5`
        );

        if (!getSongsResponse) throw new Error("Fetching songs failed");

        const data = await getSongsResponse.json();
        setSongs(data);
        //console.log(songsData);
      }

      fetchSongs();
    },
    [pageNumberSongs]
  );

  useEffect(function () {
    async function fetchTrendingSongs() {
      const getTrendingSongsResp = await fetch(
        `/api/Songs/FeaturedSongs?isFeatured=true`
      );

      if (!getTrendingSongsResp)
        throw new Error("Fetching trending songs failed");

      const data = await getTrendingSongsResp.json();
      console.log(data);
      setTrendingSongs(data);
    }

    fetchTrendingSongs();
  }, []);

  return (
    <div>
      <TrendingSong trendingSongs={trendingSongs} />
      <Artist artists={artists} />
      <input
        type="text"
        value={pageNumberArtists}
        onChange={(e) => setPageNumberArtists(e.target.value)}
      />
      <Song songs={songs} />
      <input
        type="text"
        value={pageNumberSongs}
        onChange={(e) => setPageNumberSongs(e.target.value)}
      />
    </div>
  );
}

export default App;
