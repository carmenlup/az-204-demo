import { useEffect, useState } from "react";
import { CCardText, CContainer, CRow, CCol, CImage } from "@coreui/react";

export default function ArtistDetails({ selectedId }) {
  const [artist, setArtist] = useState({});
  const [songs, setSongs] = useState([]);
  const [isLoading, setIsLoading] = useState(false);

  //distructuring
  const {
    id: artistId,
    imageUrl: artistImageUrl,
    name: artistName,
    gender: artistGender,
  } = artist;

  useEffect(
    function () {
      async function fetchArtistById() {
        setIsLoading(true);
        const artistResponse = await fetch(`/api/Artists/${selectedId}`);
        //console.log(artistResponse);

        if (!artistResponse) throw new Error("Fetching artist by id failed");

        const artistData = await artistResponse.json();
        setArtist(artistData);
        setSongs(artistData.songs);
        //console.log(artistData);
        setIsLoading(false);
      }

      fetchArtistById();
    },
    [selectedId]
  );

  return (
    <>
      {isLoading ? (
        <p className="loader">Loading...</p>
      ) : (
        <CContainer>
          <CRow>
            <CCol key={artistId}>
              <h4>Artist details:</h4>
              <div>
                Name: <b>{artistName}</b>
              </div>
              {artistGender && (
                <div>
                  Gender: <b>{artistGender}</b>
                </div>
              )}
              <div>
                {songs.length !== 0 && (
                  <div>
                    <p>Number of songs: {songs.length}</p>
                    <h4>Artist Songs: </h4>
                    {songs.map((song) => (
                      <div key={song.id}>
                        <CImage src={song.imageUrl} alt={song.title}></CImage>
                        <CCardText>{song.title}</CCardText>
                        <p>Duration:{song.duration}</p>
                      </div>
                    ))}
                  </div>
                )}
              </div>
            </CCol>
          </CRow>
        </CContainer>
      )}
    </>
  );
}
