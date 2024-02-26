import { useEffect, useState } from "react";
import "./App.css";
import "@coreui/coreui/dist/css/coreui.min.css";
import "bootstrap/dist/css/bootstrap.min.css";
import {
  CCard,
  CCardImage,
  CCardBody,
  CCardTitle,
  CCardText,
  CButton,
  CContainer,
  CRow,
  CCol,
} from "@coreui/react";
import { CAvatar } from "@coreui/react";

function App() {
  const [artists, setArtists] = useState([]);

  useEffect(function () {
    async function fetchArtists() {
      const artistsResponse = await fetch(`/api/Artists`);

      if (!artistsResponse.ok) throw new Error("Fetching artists failed");

      const getArtistsData = await artistsResponse.json();
      setArtists(getArtistsData);
      console.log(getArtistsData);
    }

    fetchArtists();
  }, []);

  return (
    <div>
      <h1>Artists</h1>
      <CContainer>
        <CRow>
          {artists?.map((artist) => (
            <CCol md={4} key={artist.id}>
              <CCard style={{ marginBottom: "1.5rem" }}>
                <CCardBody>
                  <CAvatar src={artist.imageUrl} />
                  <CCardTitle>
                    Artist <b>{artist.name}</b>
                  </CCardTitle>
                  <CCardText>short description</CCardText>
                  <CButton href="#">Details</CButton>
                </CCardBody>
              </CCard>
            </CCol>
          ))}
        </CRow>
      </CContainer>
    </div>
  );
}

export default App;
