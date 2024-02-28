import { useEffect, useState } from "react";
import { CCardText, CContainer, CRow, CCol, CImage } from "@coreui/react";

export default function ArtistDetails({ selectedId }) {
  const [songs, setSongs] = useState([]);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(
    function () {
      async function fetchArtistById() {
        setIsLoading(true);
        const artistResponse = await fetch(`/api/Artists/${selectedId}`);
        //console.log(artistResponse);

        if (!artistResponse) throw new Error("Fetching artist by id failed");

        const artistData = await artistResponse.json();
        setSongs(artistData.songs);
        //console.log(artistData.songs);
        setIsLoading(false);
      }

      fetchArtistById();
    },
    [selectedId]
  );

  return (
    <div>
      <CContainer>
        <CRow>
          {isLoading ? (
            <p className="loader">Loading...</p>
          ) : songs.length === 0 ? (
            <div className="message">No songs</div>
          ) : (
            songs.map((song) => (
              <CCol key={song.id}>
                <CImage src={song.imageUrl} alt={song.title} />
                <CCardText>{song.title}</CCardText>
              </CCol>
            ))
          )}
        </CRow>
      </CContainer>
    </div>
  );
}
