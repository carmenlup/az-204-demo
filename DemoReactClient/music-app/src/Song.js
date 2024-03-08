import {
  CCard,
  CCardImage,
  CCardBody,
  CCardTitle,
  CCardText,
  CContainer,
  CRow,
  CCol,
} from "@coreui/react";

export default function Song({ songs }) {
  return (
    <>
      <CContainer>
        <h1>New Songs</h1>
        <CRow>
          {songs?.map((song) => (
            <CCol md={2} key={song.id}>
              <CCardImage src={song.imageUrl} />
              <CCardTitle>
                <b>{song.title}</b>
              </CCardTitle>
            </CCol>
          ))}
        </CRow>
      </CContainer>
    </>
  );
}
