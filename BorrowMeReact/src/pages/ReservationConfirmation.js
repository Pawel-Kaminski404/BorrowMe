import {useEffect, useState} from "react";
import {getData} from "../services/apiFetch";
import {Link, useParams} from "react-router-dom";
import "./reservationConfirmation/reservationConfirmation.css";
import Spinner from "../components/Spinner";
import {Helmet} from "react-helmet";

const ReservationConfirmation = () => {
    const {reservationId} = useParams();
    const [reservation, setData] = useState();

    let setCorrectOtherPaymentType = (otherPaymentType) => {
        if (otherPaymentType === null){
            return "darmo";
        } else {
            return otherPaymentType;
        }
    }

    let countPrice = (price) => {
        let differenceInTime = new Date(reservation.reservationEndDay).getTime() - new Date(reservation.reservationStartDay).getTime();
        let differenceInDays = differenceInTime / (1000 * 3600 * 24);
        return Math.round(differenceInDays) * price
    }
    useEffect(() => {
        getData(`/api/reservations/${reservationId}`)
            .then(data => {
                setData(data);
                console.log(data)
            })
    }, [])

    return (
        <div className="reservation-confirmation-container">
            <Helmet>
                <title>Potwierdzenie rezerwacji | BorrowMe</title>
            </Helmet>
            {reservation ?
                <>
                    <div className="confirmation-top center">
                        <label id="title-label">Potwierdzenie rezerwacji o id: {reservationId}</label>
                    </div>
                    <div className="confirmation-middle">
                        <div className="confirmation-middle-deeper">
                            <label id="confirmation-details-label">Szczegoły rezerwacji:</label>
                            <div className="confirmation-details-container">
                                <label>Rezerwujesz:</label>
                                <p><Link className="reference-link"
                                         to={"/announcement/" + reservation.announcement.id}>{reservation.announcement.title}</Link>
                                </p>
                                <label>Od użytkownika:</label>
                                <p><Link className="reference-link"
                                         to={"/users/" + reservation.announcement.owner.id}>{reservation.announcement.owner.firstName} {reservation.announcement.owner.lastName}</Link>
                                </p>
                                <label>Zarezerwowane od:</label>
                                <p>{new Date(reservation.reservationStartDay).toLocaleDateString()}</p>
                                <label>Zarezerwowane do:</label>
                                <p>{new Date(reservation.reservationEndDay).toLocaleDateString()}</p>
                                <label>Cena rezerwacji:</label>
                                <p>{reservation.announcement.paymentType===1 ? countPrice(reservation.announcement.price) + " zł" : "Za "+setCorrectOtherPaymentType(reservation.announcement.otherPaymentType)}</p>
                                <label id="confirmation-info-label">Na swoją pocztę dostaniesz powiadomienie,
                                    gdy ogłoszeniodawca zatwierdzi Twoją rezerwację.</label>
                            </div>
                        </div>
                    </div>
                    <div className="confirmation-bottom center">
                        <button className="btn shadow-none homepage-button">
                            <Link className="home-page-link" to="/">Wróć do strony głownej</Link>
                        </button>
                    </div>
                </>
                : <Spinner/>}
        </div>

    )
}

export default ReservationConfirmation
