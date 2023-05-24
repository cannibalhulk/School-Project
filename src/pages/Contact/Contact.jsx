import React, { useState } from 'react';
import emailjs, { init } from 'emailjs-com';
import "./Contact.css";
import contactImg from "../../assets/images/contact-us.jpg";

const Contact = () => {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [message, setMessage] = useState('');
    const [emailSent, setEmailSent] = useState(false);

    const isValidEmail = email => {
        const regex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return regex.test(String(email).toLowerCase());
    };

    const submit = () => { 
        if (name && email && message && isValidEmail(email)) {
            emailjs.init('HKyffi3U-f8r8qO5b');
            const serviceId = 'service_wl2pi6e';
            const templateId = 'template_krxxft4';
            const templateParams = {
                name,
                email,
                message
            }; 
             
            emailjs.send(serviceId, templateId, templateParams)
                .then(response => console.log(response))
                .then(error => console.log(error));

            setName('');
            setEmail('');
            setMessage('');
            setEmailSent(true); 
        } else {
            alert('Please fill in all fields.');
        } 
    }
    return (
        <section className='contact'>
            <div className='container'>
                <div className='section-title'>
                    <h2 className='fs-26'>Bizimlə əlaqə qur</h2>
                </div>

                <div className='contact-content grid'>
                    <div className='contact-img'>
                        <img src={contactImg} alt="" />
                    </div>
                    <div id="contact-form">
                        <input className='fs-20 name' type="text" placeholder="Adınız" value={name} onChange={e => setName(e.target.value)} />
                        <input className='fs-20 adress' type="email" placeholder="Email ünvanınız" value={email} onChange={e => setEmail(e.target.value)} />
                        <textarea className='fs-22 message ' placeholder="Mesajınız" value={message} onChange={e => setMessage(e.target.value)}></textarea>
                        <input type={'button'} className='fs-26 ls-1 send' onClick={submit} value="Mesaj göndər" />
                        <span className={emailSent ? 'visible' : null}>Mesajın üçün təşəkkürlər. Qısa zaman ərzində geri dönüş edəcəyik!</span>
                    </div>
                </div>
            </div> 
        </section>

    );
};
export default Contact
