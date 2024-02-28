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
    <div>
      <h1>Songs</h1>
      <CContainer>
        <CRow>
          {songs?.map((song) => (
            <CCol md={2} key={song.id}>
              <CCard style={{ marginBottom: "1.5rem" }}>
                <CCardBody>
                  {/* <CAvatar src={artist.imageUrl} /> */}
                  <CCardImage src={song.imageUrl} />
                  {/* <CCardImage src={song.audioUrl} /> */}
                  <CCardTitle>
                    <b>{song.title}</b>
                  </CCardTitle>
                  <CCardText>Duration :{song.duration}</CCardText>
                </CCardBody>
              </CCard>
            </CCol>
          ))}
        </CRow>
      </CContainer>
    </div>
  );
}
