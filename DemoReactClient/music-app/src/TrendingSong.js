import { CContainer, CRow, CCol, CCardImage, CCardTitle } from "@coreui/react";

export default function TrendingSongs({ trendingSongs }) {
  return (
    <>
      <CContainer>
        <h1>Trending</h1>
        <CRow>
          {trendingSongs?.map((song) => (
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
