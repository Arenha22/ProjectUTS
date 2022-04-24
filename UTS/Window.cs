using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace UTS
{
    static class Constants
    {
        public const string path = "../../../Shaders/";
    }

    internal class Window : GameWindow
    {
        List<Asset3d> _objectList = new List<Asset3d>();
        float degr = 0;
        double _time = 0;
        Camera _camera;
        bool _firstMove = true;
        Vector2 _lastPost;
        Vector3 _objectPos = new Vector3(0, 0, 0);
        float _rotationSpeed = 1f;
        List<Asset3d> _objectListBlack = new List<Asset3d>();
        List<Asset3d> _hairList = new List<Asset3d>();
        Asset3d blackbird;
        List<Asset3d> _objectListRed = new List<Asset3d>();
        bool isJumped = false;
        bool dash = false;
        int i = 5;
        int j = 5;
        int x = 10;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            //ganti background warna
            GL.Enable(EnableCap.DepthTest);//kasih tau kau ada object dibelakang gak keliatan
            GL.ClearColor(0f, 0.75f, 1f, 1.0f);

            //Green Pig
            var ellipse1 = new Asset3d(new Vector3(0f, 0.9f, 0));//perut1
            ellipse1.createEllipsoid(0f, 0.5f, 0f, 0.5f, 0.5f, 0.5f, 72, 24);//badan
            _objectList.Add(ellipse1);

            var ellipse2 = new Asset3d(new Vector3(1f, 1f, 1f));//mata kiri
            ellipse2.createEllipsoid(-0.2f, 0.6f, 0.35f, 0.13f, 0.13f, 0.13f, 72, 24);//mata kiri1
            _objectList.Add(ellipse2);
            ellipse1.child.Add(ellipse2);

            var ellipse3 = new Asset3d(new Vector3(1f, 1f, 1f));//mata kanan
            ellipse3.createEllipsoid(0.2f, 0.6f, 0.35f, 0.13f, 0.13f, 0.13f, 72, 24);//mata kanan1
            _objectList.Add(ellipse3);
            ellipse1.child.Add(ellipse3);

            var ellipse4 = new Asset3d(new Vector3(0f, 0f, 0f));//mata kanan2
            ellipse4.createEllipsoid(0.27f, 0.6f, 0.43f, 0.05f, 0.05f, 0.05f, 72, 24);//mata kanan2
            _objectList.Add(ellipse4);
            ellipse1.child.Add(ellipse4);

            var ellipse5 = new Asset3d(new Vector3(0, 0, 0));//mata kiri2
            ellipse5.createEllipsoid(-0.27f, 0.6f, 0.43f, 0.05f, 0.05f, 0.05f, 72, 24);//mata kiri2
            _objectList.Add(ellipse5);
            ellipse1.child.Add(ellipse5);

            var ellipse6 = new Asset3d(new Vector3(0.1f, 0.7f, 0));//Telinga kiri
            ellipse6.createEllipsoid(-0.3f, 0.9f, 0.15f, 0.1f, 0.12f, 0.1f, 72, 24);//Telinga kiri
            _objectList.Add(ellipse6);
            ellipse1.child.Add(ellipse6);

            var ellipse7 = new Asset3d(new Vector3(0.1f, 0.7f, 0));//Telinga kanan
            ellipse7.createEllipsoid(0.3f, 0.9f, 0.15f, 0.1f, 0.12f, 0.1f, 72, 24);//Telinga kanan
            _objectList.Add(ellipse7);
            ellipse1.child.Add(ellipse7);

            var kotak1 = new Asset3d(new Vector3(0f, 0.7f, 0));//alis kiri
            kotak1.createBoxVertices(-0.11f, 0.65f, 0.06f, 0.35f);
            kotak1.rotate(kotak1.objectCenter, Vector3.UnitX, 13);
            kotak1.rotate(kotak1.objectCenter, Vector3.UnitZ, 2);
            _objectList.Add(kotak1);
            ellipse1.child.Add(kotak1);

            var kotak2 = new Asset3d(new Vector3(0f, 0.7f, 0));//alis kanan
            kotak2.createBoxVertices(0.11f, 0.65f, 0.06f, 0.35f);
            kotak2.rotate(kotak2.objectCenter, Vector3.UnitX, 13);
            kotak2.rotate(kotak2.objectCenter, Vector3.UnitZ, -2);
            _objectList.Add(kotak2);
            ellipse1.child.Add(kotak2);

            var ellipse9 = new Asset3d(new Vector3(0f, 0.1f, 0));//Bolongan kiri
            ellipse9.createEllipsoid(-0.07f, 0.45f, 0.06f, 0.15f, 0.2f, 0.5f, 72, 24);
            _objectList.Add(ellipse9);
            ellipse1.child.Add(ellipse9);

            var ellipse10 = new Asset3d(new Vector3(0f, 0.1f, 0));//Bolongan kanan
            ellipse10.createEllipsoid(0.07f, 0.45f, 0.06f, 0.15f, 0.2f, 0.5f, 72, 24);
            _objectList.Add(ellipse10);
            ellipse1.child.Add(ellipse10);

            var ellipse11 = new Asset3d(new Vector3(0f, 0.5f, 0));//Telinga kiri
            ellipse11.createEllipsoid(-0.3f, 0.88f, 0.225f, 0.04f, 0.06f, 0.04f, 72, 24);//Telinga kiri
            _objectList.Add(ellipse11);
            ellipse1.child.Add(ellipse11);

            var ellipse12 = new Asset3d(new Vector3(0f, 0.5f, 0));//Telinga kanan
            ellipse12.createEllipsoid(0.3f, 0.88f, 0.225f, 0.04f, 0.06f, 0.04f, 72, 24);//Telinga kanan
            _objectList.Add(ellipse12);
            ellipse1.child.Add(ellipse12);

            var torus1 = new Asset3d(new Vector3(0.9f, 0.9f, 0));//Angel Cap
            torus1.createTorus(0f, 1.2f, 0f, 0.2f, 0.025f, 72, 24);
            _objectList.Add(torus1);
            ellipse1.child.Add(torus1);


            //BlackBird
            var blackbird = new Asset3d(new Vector3(0, 0, 0));
            blackbird.createEllipsoid(0f, 0.5f, 0f, 0.5f, 0.55f, 0.5f, 72, 24);
            _objectListBlack.Add(blackbird);

            //mata abu kiri
            var mataAbuKiri = new Asset3d(new Vector3(0.47f, 0.47f, 0.47f));
            mataAbuKiri.createEllipsoid(-0.2f, 0.6f, 0.35f, 0.15f, 0.15f, 0.15f, 72, 24);
            blackbird.child.Add(mataAbuKiri);
            _objectListBlack.Add(mataAbuKiri);

            //mata abu kanan
            var mataAbuKanan = new Asset3d(new Vector3(0.47f, 0.47f, 0.47f));
            mataAbuKanan.createEllipsoid(0.2f, 0.6f, 0.35f, 0.15f, 0.15f, 0.15f, 72, 24);
            blackbird.child.Add(mataAbuKanan);
            _objectListBlack.Add(mataAbuKanan);

            //mata putih kiri
            var mataPutihKiri = new Asset3d(new Vector3(1, 1, 1));
            mataPutihKiri.createEllipsoid(-0.22f, 0.6f, 0.42f, 0.1f, 0.1f, 0.1f, 72, 24);
            blackbird.child.Add(mataPutihKiri);
            _objectListBlack.Add(mataPutihKiri);

            //mata putih kanan
            var mataPutihKanan = new Asset3d(new Vector3(1, 1, 1));
            mataPutihKanan.createEllipsoid(0.22f, 0.6f, 0.42f, 0.1f, 0.1f, 0.1f, 72, 24);
            blackbird.child.Add(mataPutihKanan);
            _objectListBlack.Add(mataPutihKanan);

            //mata hitam kiri
            var mataHitamKiri = new Asset3d(new Vector3(0, 0, 0));
            mataHitamKiri.createEllipsoid(-0.22f, 0.6f, 0.5f, 0.04f, 0.04f, 0.04f, 72, 24);
            blackbird.child.Add(mataHitamKiri);
            _objectListBlack.Add(mataHitamKiri);

            //mata hitam kanan
            var mataHitamKanan = new Asset3d(new Vector3(0, 0, 0));
            mataHitamKanan.createEllipsoid(0.22f, 0.6f, 0.5f, 0.04f, 0.04f, 0.04f, 72, 24);
            blackbird.child.Add(mataHitamKanan);
            _objectListBlack.Add(mataHitamKanan);

            //tanda putih
            var tandaPutih = new Asset3d(new Vector3(1, 1, 1));
            tandaPutih.createEllipsoid(0.04f, 0.7f, 0.43f, 0.05f, 0.05f, 0.05f, 72, 24);
            blackbird.child.Add(tandaPutih);
            _objectListBlack.Add(tandaPutih);

            //paruh atas
            var paruhAtas = new Asset3d(new Vector3(1, 1, 0));
            paruhAtas.createElipticCone(0.6f, 0f, -0.2f, 0.05f, 0.05f, 0.05f);
            paruhAtas.rotate(paruhAtas.objectCenter, Vector3.UnitZ, 90);
            paruhAtas.rotate(paruhAtas.objectCenter, Vector3.UnitX, 68);
            blackbird.child.Add(paruhAtas);
            _objectListBlack.Add(paruhAtas);

            //paruh bawah
            var paruhBawah = new Asset3d(new Vector3(1, 1, 0));
            paruhBawah.createElipticCone(-0.05f, 0f, 0.38f, 0.05f, 0.05f, 0.05f);
            paruhBawah.rotate(paruhBawah.objectCenter, Vector3.UnitZ, 90);
            paruhBawah.rotate(paruhBawah.objectCenter, Vector3.UnitX, 200);
            paruhBawah.rotate(paruhBawah.objectCenter, Vector3.UnitY, 180);
            blackbird.child.Add(paruhBawah);
            _objectListBlack.Add(paruhBawah);

            //alis kiri
            var alisKiri = new Asset3d(new Vector3(1, 0.73f, 0));
            alisKiri.createBoxVertices2(-0.4f, 0.72f, 0.4f, 0.35f, 0.05f, 0.05f);
            alisKiri.rotate(alisKiri.objectCenter, Vector3.UnitZ, -15);
            blackbird.child.Add(alisKiri);
            _objectListBlack.Add(alisKiri);

            //alis kanan
            var alisKanan = new Asset3d(new Vector3(1, 0.73f, 0));
            alisKanan.createBoxVertices2(0.4f, 0.72f, 0.4f, 0.35f, 0.05f, 0.05f);
            alisKanan.rotate(alisKanan.objectCenter, Vector3.UnitZ, 15);
            blackbird.child.Add(alisKanan);
            _objectListBlack.Add(alisKanan);

            //rambut
            var rambut = new Asset3d(new Vector3(1, 1, 0));
            rambut.AddCoordinates(0, 0.9f, 0);
            rambut.AddCoordinates(0f, 1.2f, 0);
            rambut.AddCoordinates(0f, 1.25f, 0);
            rambut.AddCoordinates(0.2f, 1.3f, 0);
            rambut.Bezier3d();
            blackbird.child.Add(rambut);
            _hairList.Add(rambut);


            //RedBird
            var red1 = new Asset3d(new Vector3(1f, 0f, 0f));//badan
            red1.createEllipsoid(0f, 0.5f, 0f, 0.5f, 0.55f, 0.5f, 72, 24);
            _objectListRed.Add(red1);

            var red2 = new Asset3d(new Vector3(1f, 1f, 1f));//mata kiri1
            red2.createEllipsoid(-0.1f, 0.6f, 0.4f, 0.13f, 0.13f, 0.13f, 72, 24);
            red1.child.Add(red2);

            var red3 = new Asset3d(new Vector3(1f, 1f, 1f));//mata kanan1
            red3.createEllipsoid(0.1f, 0.6f, 0.4f, 0.13f, 0.13f, 0.13f, 72, 24);
            red1.child.Add(red3);

            var red4 = new Asset3d(new Vector3(0f, 0f, 0f));//pupil kiri
            red4.createEllipsoid(-0.1f, 0.6f, 0.5f, 0.05f, 0.05f, 0.05f, 72, 24);
            red1.child.Add(red4);

            var red5 = new Asset3d(new Vector3(0f, 0f, 0f));//pupil kanan
            red5.createEllipsoid(0.1f, 0.6f, 0.5f, 0.05f, 0.05f, 0.05f, 72, 24);
            red1.child.Add(red5);

            var box = new Asset3d(new Vector3(0, 0f, 0));//alis kanan
            box.createBoxVertices2(0.3f, 0.7f, 0.42f, 0.3f, 0.09f, 0.09f);
            box.rotate(box.objectCenter, Vector3.UnitZ, 15);
            red1.child.Add(box);

            var box2 = new Asset3d(new Vector3(0, 0f, 0));//alis kiri
            box2.createBoxVertices2(-0.3f, 0.7f, 0.42f, 0.3f, 0.09f, 0.09f);
            box2.rotate(box2.objectCenter, Vector3.UnitZ, -15);
            red1.child.Add(box2);

            var cone1 = new Asset3d(new Vector3(1, 1, 0));//mulut atas
            cone1.createElipticCone(0.6f, 0f, -0.2f, 0.05f, 0.05f, 0.05f);
            cone1.rotate(cone1.objectCenter, Vector3.UnitZ, 90);
            cone1.rotate(cone1.objectCenter, Vector3.UnitX, 68);
            //Asset3d.child.Add(cone1);
            red1.child.Add(cone1);
            var cone2 = new Asset3d(new Vector3(1, 1, 0));//mulut bawah
            cone2.createElipticCone(-0.05f, 0f, 0.425f, 0.05f, 0.05f, 0.05f);
            cone2.rotate(cone2.objectCenter, Vector3.UnitZ, 90);
            cone2.rotate(cone2.objectCenter, Vector3.UnitX, 200);
            cone2.rotate(cone2.objectCenter, Vector3.UnitY, 180);
            //blackbird.child.Add(cone2);
            red1.child.Add(cone2);

            var silinder1 = new Asset3d(new Vector3(1f, 0f, 0f));//rambut kecil
            silinder1.createCylinder(0.05f, 1.1f, -0.1f, 0.05f, 0.175f);
            silinder1.rotate(silinder1.objectCenter, Vector3.UnitX, -20);
            red1.child.Add(silinder1);
            var silinder2 = new Asset3d(new Vector3(1f, 0f, 0f));//rambut besar
            silinder2.createCylinder(0f, 1.1f, 0.025f, 0.05f, 0.4f);
            silinder2.rotate(silinder2.objectCenter, Vector3.UnitX, -20);
            red1.child.Add(silinder2);

            var box3 = new Asset3d(new Vector3(0, 0f, 0));//ekor tengah
            box3.createBoxVertices2(0f, 0.2f, -0.5f, 0.09f, 0.6f, 0f);
            red1.child.Add(box3);

            var box4 = new Asset3d(new Vector3(0, 0f, 0));//ekor kanan
            box4.createBoxVertices2(0.025f, 0.2f, -0.5f, 0.09f, 0.3f, 0f);
            box4.rotate(box4.objectCenter, Vector3.UnitY, -15);
            red1.child.Add(box4);

            var box5 = new Asset3d(new Vector3(0, 0f, 0));//ekor kiri
            box5.createBoxVertices2(-0.025f, 0.2f, -0.5f, 0.09f, 0.3f, 0f);
            box5.rotate(box5.objectCenter, Vector3.UnitY, 15);
            red1.child.Add(box5);


            _camera = new Camera(new Vector3(0, 0, 5), Size.X / Size.Y);
            CursorGrabbed = true;

            foreach (Asset3d i in _objectList)
            {
                i.load(Size.X, Size.Y, Constants.path + "shader.vert", Constants.path + "shader.frag");
            }

            foreach (Asset3d i in _objectListBlack)
            {
                i.load(Size.X, Size.Y, Constants.path + "shader.vert", Constants.path + "shader.frag");
            }
            foreach (Asset3d i in _hairList)
            {
                i.load(Size.X, Size.Y, Constants.path + "shader.vert", Constants.path + "shader.frag");
            }

            foreach (Asset3d i in _objectListRed)
            {
                i.load(Size.X, Size.Y, Constants.path + "shader.vert", Constants.path + "shader.frag");
            }
            foreach (Asset3d i in red1.child)
            {
                i.load(Size.X, Size.Y, Constants.path + "shader.vert", Constants.path + "shader.frag");
            }

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            //GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _time += 7.0 * args.Time;
            var temp = Matrix4.Identity;
            degr += MathHelper.DegreesToRadians(0.5f);
            temp = temp * Matrix4.CreateRotationZ(degr);

            foreach (Asset3d i in _objectList)
            {
                i.render(_time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            }

            foreach (Asset3d i in _hairList)
            {
                i.render(_time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix(), "lineBezier");
            }

            foreach (Asset3d i in _objectListBlack)
            {
                i.render(_time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            }

            foreach (Asset3d i in _objectListRed)
            {
                i.render(_time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            }
            foreach (Asset3d i in _objectListRed[0].child)
            {
                i.render(_time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            }

            _objectList[0].rotate(_objectList[0].objectCenter, Vector3.UnitY, 3);

            _objectListBlack[0].rotate(_objectListBlack[0].objectCenter, Vector3.UnitY, 3);

            _objectListRed[0].rotate(_objectListRed[0].objectCenter, Vector3.UnitY, 3);

            //if (x >= 0)    //posisi awal sejajar
            //{
            //    _objectListRed[0].translate(0.4f,0f, 0f);
            //    _objectListBlack[0].translate(-0.4f, 0f, 0f);
            //    x -= 1;
            //}

            if (x >= 0) //posisi awal untuk melakukan dash
            {
                _objectListRed[0].translate(-0.5f, -0.2f, 0f);
                _objectListBlack[0].translate(-0.35f, 0f, 0f);
                x -= 1;
            }

            SwapBuffers();

        }


        public Matrix4 generateArbRotationMatrix(Vector3 axis, Vector3 center, float degree)
        {
            var rads = MathHelper.DegreesToRadians(degree);

            var secretFormula = new float[4, 4] {
                { (float)Math.Cos(rads) + (float)Math.Pow(axis.X, 2) * (1 - (float)Math.Cos(rads)), axis.X* axis.Y * (1 - (float)Math.Cos(rads)) - axis.Z * (float)Math.Sin(rads),    axis.X * axis.Z * (1 - (float)Math.Cos(rads)) + axis.Y * (float)Math.Sin(rads),   0 },
                { axis.Y * axis.X * (1 - (float)Math.Cos(rads)) + axis.Z * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Y, 2) * (1 - (float)Math.Cos(rads)), axis.Y * axis.Z * (1 - (float)Math.Cos(rads)) - axis.X * (float)Math.Sin(rads),   0 },
                { axis.Z * axis.X * (1 - (float)Math.Cos(rads)) - axis.Y * (float)Math.Sin(rads),   axis.Z * axis.Y * (1 - (float)Math.Cos(rads)) + axis.X * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Z, 2) * (1 - (float)Math.Cos(rads)), 0 },
                { 0, 0, 0, 1}
            };
            var secretFormulaMatix = new Matrix4
            (
                new Vector4(secretFormula[0, 0], secretFormula[0, 1], secretFormula[0, 2], secretFormula[0, 3]),
                new Vector4(secretFormula[1, 0], secretFormula[1, 1], secretFormula[1, 2], secretFormula[1, 3]),
                new Vector4(secretFormula[2, 0], secretFormula[2, 1], secretFormula[2, 2], secretFormula[2, 3]),
                new Vector4(secretFormula[3, 0], secretFormula[3, 1], secretFormula[3, 2], secretFormula[3, 3])
            );

            return secretFormulaMatix;
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            _camera.Fov = _camera.Fov - e.OffsetY;
        }



        // dijalani setiap ada reziew window
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        // dijalani setiap 60 fps 
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            var input = KeyboardState;      //input keyboard
            var mouse_input = MouseState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            float cameraSpeed = 2.5f;
            if (KeyboardState.IsKeyDown(Keys.W))
            {
                Console.WriteLine("W");
                _camera.Position += _camera.Front * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.Space))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)args.Time;
            }

            var mouse = MouseState;
            var sensitivity = 0.1f;

            if (_firstMove)
            {
                _lastPost = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;

            }
            else
            {
                var deltaX = mouse.X - _lastPost.X;
                var deltaY = mouse.Y - _lastPost.Y;
                _lastPost = new Vector2(mouse.X, mouse.Y);
                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch += deltaY * sensitivity;
            }

            if (KeyboardState.IsKeyDown(Keys.T))
            {
                _objectList[0].translate(0f, 0.05f, 0f);
            }
            if (KeyboardState.IsKeyDown(Keys.G))
            {
                _objectList[0].translate(0f, -0.05f, 0f);
            }
            if (KeyboardState.IsKeyDown(Keys.F))
            {
                _objectList[0].translate(-0.05f, 0f, 0f);
            }
            if (KeyboardState.IsKeyDown(Keys.H))
            {
                _objectList[0].translate(0.05f, 0f, 0f);
            }


            if (KeyboardState.IsKeyDown(Keys.Up))
            {
                _objectListBlack[0].translate(0f, 0.05f, 0f);
            }
            if (KeyboardState.IsKeyDown(Keys.Down))
            {
                _objectListBlack[0].translate(0f, -0.1f, 0f);
            }
            if (KeyboardState.IsKeyDown(Keys.Left))
            {
                _objectListBlack[0].translate(-0.05f, 0f, 0f);
            }
            if (KeyboardState.IsKeyDown(Keys.Right))
            {
                _objectListBlack[0].translate(0.05f, 0f, 0f);
            }


            if (KeyboardState.IsKeyDown(Keys.I))
            {
                _objectListRed[0].translate(0f, 0.05f, 0f);
            }
            if (KeyboardState.IsKeyDown(Keys.K))
            {
                _objectListRed[0].translate(0f, -0.05f, 0f);
            }
            if (KeyboardState.IsKeyDown(Keys.J))
            {
                _objectListRed[0].translate(-0.05f, 0f, 0f);
            }
            if (KeyboardState.IsKeyDown(Keys.L))
            {
                _objectListRed[0].translate(0.05f, 0f, 0f);
            }
            if (KeyboardState.IsKeyDown(Keys.U))
            {
                _objectListRed[0].translate(0f, 0f, 0.05f);
            }
            if (KeyboardState.IsKeyDown(Keys.O))
            {
                _objectListRed[0].translate(0f, 0f, -0.05f);
            }
            if (input.IsKeyDown(Keys.P))
            {
                if (!isJumped)
                {
                    isJumped = true;
                    i = 20;
                    j = 20;
                }
            }
            if (isJumped)
            {

                if (i >= 0)
                {
                    _objectListRed[0].translate(0f, 0.1f, 0f);
                    _objectList[0].translate(0f, 0.1f, 0f);
                    _objectListBlack[0].translate(0f, 0.1f, 0f);
                    i -= 1;
                }
                if (j >= 0 && i < 0)
                {
                    _objectListRed[0].translate(0f, -0.1f, 0f);
                    _objectList[0].translate(0f, -0.1f, 0f);
                    _objectListBlack[0].translate(0f, -0.1f, 0f);

                    j -= 1;
                }
                if (j < 0)
                {
                    isJumped = false;
                }

            }

            //Untuk melakukan Dash
            if (input.IsKeyDown(Keys.Y))
            {
                if (!dash)
                {
                    dash = true;
                    i = 50;
                    j = 30;
                }
            }

            if (dash)
            {
                if (i >= 0)
                {
                    _objectListRed[0].translate(0.2f, 0.1f, 0f);
                    i -= 1;
                    if (_objectListRed[0].objectCenter.Y >= 1)
                    {
                        i = -1;
                    }
                    _objectListBlack[0].translate(0.15f, 0f, 0f);
                    i -= 1;
                    if (_objectListRed[0].objectCenter.Y >= 1)
                    {
                        i = -1;
                    }


                }
                if (j >= 0 && i < 0)
                {

                    _objectListRed[0].translate(0f, -0.1f, 0f);
                    _objectListBlack[0].translate(-0.1f, 0.05f, 0f);
                    _objectList[0].translate(0.2f, -0.1f, 0f);
                    j -= 1;
                    if (_objectListRed[0].objectCenter.Y <= -5)
                    {
                        j = -1;
                    }
                }
                if (j < 0)
                {
                    dash = false;
                }
            }
            //Untuk Melakukan Spin
            if (input.IsKeyDown(Keys.R))
            {
                _objectList[0].rotate(_objectList[0]._centerPosition, Vector3.UnitY, 3);

                _objectListBlack[0].rotate(_objectListBlack[0]._centerPosition, Vector3.UnitY, 3);

                _objectListRed[0].rotate(_objectListRed[0]._centerPosition, Vector3.UnitY, 3);

            }
            /*
            protected override void OnMouseDown(MouseButtonEventArgs e)
            {
                base.OnMouseDown(e);
                if(e.Button == MouseButton.Left)
                {
                    float _x = (MousePosition.X - Size.X/2) / (Size.X/2);
                    float _y = -(MousePosition.Y - Size.Y/2) / (Size.Y/2);

                    Console.WriteLine(_x + " " + _y);

                    _object[6].updateMousePosition(_x, _y);
                }
            }
            */
        }
    }
}
