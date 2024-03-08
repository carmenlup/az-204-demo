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
      <CContainer>
        <h1>Artists</h1>
        <CRow>
          {artists?.map((artist) => (
            <CCol key={artist.id} onClick={() => handleSelectArtist(artist.id)}>
              <CAvatar src={artist.imageUrl} size="xl" />
              <CCardTitle style={{ margin: "1rem" }}>
                <b>{artist.name}</b>
              </CCardTitle>
            </CCol>
          ))}
        </CRow>
      </CContainer>
      {selectedId ? <ArtistDetails selectedId={selectedId} /> : null}
    </>
  );
}
