import {Link} from "react-router-dom";

const Footer = ({footerRef}) => {
    return (
        <div id="footer" className="footer text-left mt-auto" ref={footerRef}>
            <div className="container p-4">
                <div className="row">
                    <div className="col-auto">
                        <p className="text-muted">© 2022 Copyright: BorrowMe.pl</p>
                    </div>
                    <div className="col-auto">
                        <Link className="text-light no-underline" to="/terms-of-use">Regulamin</Link>
                    </div>
                    <div className="col-auto">
                        <Link className="text-light no-underline" to="/privacy-policy">Polityka prywatności</Link>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Footer;