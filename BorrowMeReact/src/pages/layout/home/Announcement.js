import '../../../styles/announcement.css'
import ImageAPI from "../../../components/ImageAPI";

const Announcement = ({ announcement }) => {
  return (
    <div className='announcement'>
      {console.log(announcement)}
      <div className="title">
        <b>{announcement.title}</b>
      </div>
      <div className="picture">
        <ImageAPI imageDirectory={announcement.pictureLocation.directoryName} imageName={announcement.pictureLocation.fileName} />
      </div>
      <div className="informations-row-1">
        <div className="price">
          10PLN
        </div>
        <div className="city">
          {announcement.city.name}
        </div>
      </div>
      <div className="informations-row-2">
        <div className="date">
          {announcement.publishDate.slice(0, 10)}
        </div>
      </div>
    </div>
  )
}



export default Announcement