import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { AiFillEye, AiFillEyeInvisible } from "react-icons/ai";

export default function Login() {

    const [showPassword, setShowPassword] = useState(false);
    const [formData, setFormData] = useState({
        staffNumber: "",
        password: "",
    });

    const { staffNumber, password } = formData;
    const navigate = useNavigate();

    function onChange(e) {
        setFormData((prevState) => ({
            ...prevState,
            [e.target.id]: e.target.value,
        })
        );
    }

    async function onSubmit(e){
        e.preventDefault();

        navigate("/")
    }
    return (
        <section>
            <h1 className="text-3xl text-center mt-6 font-bold">Login</h1>

            <div className="flex justify-center flex-wrap items-center px-6 py-12 max-w-6xl mx-auto">

                <div className="w-full md:w-[67%] lg:w-[40%] lg:ml-20">
                    <form onSubmit={onSubmit}>
                        <input type="text" id="staffNumber" value={staffNumber} onChange={onChange} placeholder="Staff Number" className="mb-6 w-full px-4 py-2 text-xl text-gray-700 bg-white border-gray-300 rounded transition ease-in-out" />

                        <div className="relative mb-6">
                            <input type={showPassword ? "text" : "password"} id="password" value={password} onChange={onChange} placeholder="Password" className="w-full px-4 py-2 text-xl text-gray-700 bg-white border-gray-300 rounded transition ease-in-out" />

                            {showPassword ? (
                                <AiFillEyeInvisible className="absolute right-3 top-3 text-xl cursor-pointer" onClick={() => setShowPassword((prevState) => !prevState)} />
                            ) : (
                                <AiFillEye className="absolute right-3 top-3 text-xl cursor-pointer" onClick={() => setShowPassword((prevState) => !prevState)} />
                            )}
                        </div>


                        <div className="flex justify-between whitespace-nowrap text-sm sm:text-lg">
                            <p>
                                <Link to="/reset-request" className="text-blue-600 hover:text-blue-800 transition duration-200 ease-in-out">Forgot Password?</Link>
                            </p>
                        </div>

                        <button type="submit" className="w-full bg-blue-600 text-white px-7 py-3 text-sm font-medium uppercase rounded shadow-md hover:bg-blue-700 transition duration-150 ease-in-out hover:shadow-lg active:bg-blue-800"><Link to="/home" >Login</Link></button>

                    </form>
                </div>
            </div>
        </section>
    )
}