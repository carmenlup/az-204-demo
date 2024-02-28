import {
  CCard,
  CCardBody,
  CCardTitle,
  CCardText,
  CButton,
  CContainer,
  CRow,
  CCol,
  CAvatar,
} from "@coreui/react";
import { useEffect, useState } from "react";
import ArtistDetails from "./ArtistDetails";

export default function Artist({ artists }) {
  const [selectedId, setSelectedId] = useState(null);

  function handleSelectArtist(id) {
    setSelectedId((selectedId) => (id === selectedId ? null : id));
  }
  return (
    <>
      <h1>Artists</h1>
      <CContainer>
        <CRow>
          {artists?.map((artist) => (
            <CCol key={artist.id}>
              <CCard style={{ marginBottom: "1.5rem" }}>
                <CCardBody>
                  <CAvatar src={artist.imageUrl} />
                  <CCardTitle>
                    Artist <b>{artist.name}</b>
                  </CCardTitle>
                  <CCardText>short description</CCardText>
                  <CButton onClick={() => handleSelectArtist(artist.id)}>
                    More
                  </CButton>
                </CCardBody>
              </CCard>
            </CCol>
          ))}
        </CRow>
      </CContainer>
      {selectedId ? <ArtistDetails selectedId={selectedId} /> : null}
    </>
  );
}
